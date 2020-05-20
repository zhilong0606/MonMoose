using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditorInternal;

namespace MonMoose.Core
{
    public class LayerTools
    {
        [MenuItem("Tools/Layer/Generate LayerMask Window")]
        public static void ShowWindow()
        {
            LayerGenerateWindow window = EditorWindow.GetWindow<LayerGenerateWindow>(true, "Generate LayerMask Window");
            window.Init();
        }
    }


    public class LayerGenerateWindow : EditorWindow
    {
        private const string LayerFolderPath = "/Scripts/GameCore/Layer/";
        private const string LayerEditorFolderPath = LayerFolderPath + "Editor/";
        private const string LayerInfoFilePath = LayerEditorFolderPath + "LayerInfo.xml";
        private const string LayerCacheFilePath = LayerEditorFolderPath + "LayerMaskCache.txt";
        private const string GenerateFilePath = LayerFolderPath + "LayerUtilityGen.cs";
        private const float NameFieldWidth = 120f;
        private const float CheckBoxWidth = 50f;

        private ReorderableList reorderableList;
        private Vector2 generateButtonSize = new Vector2(100f, 20f);
        private Vector2 generateButtonOffset = new Vector2(110, 30f);
        private Rect generateButtonRect;
        private List<MaskCell> maskCellList = new List<MaskCell>();
        private List<LayerInfo> layerInfoList = new List<LayerInfo>();
        private Dictionary<string, int> layerInfoIndexCache = new Dictionary<string, int>();
        private StringBuilder stringBuilder = new StringBuilder();
        private int tabIndex = 0;

        private void OnGUI()
        {
            if (reorderableList == null)
            {
                reorderableList = new ReorderableList(maskCellList, typeof(MaskCell), true, true, true, true);
                reorderableList.drawHeaderCallback = OnMaskCellHeaderDraw;
                reorderableList.drawElementCallback = OnMaskCellElementDraw;
                reorderableList.onAddCallback = OnMaskCellAdd;
                reorderableList.onRemoveCallback = OnMaskCellRemove;
            }
            reorderableList.DoLayoutList();
            if (GUI.Button(generateButtonRect, "Generate"))
            {
                Generate();
            }
        }

        public void Init()
        {
            InitLayerInfoList();
            InitWindowSize();
            LoadCache();
        }

        private void InitLayerInfoList()
        {
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty it = tagManager.GetIterator();
            while (it.NextVisible(true))
            {
                if (it.name == "layers")
                {
                    for (int i = 0; i < it.arraySize; i++)
                    {
                        string layerName = it.GetArrayElementAtIndex(i).stringValue;
                        if (string.IsNullOrEmpty(layerName))
                        {
                            continue;
                        }
                        LayerInfo info = new LayerInfo();
                        info.id = i;
                        info.name = it.GetArrayElementAtIndex(i).stringValue;
                        layerInfoIndexCache.Add(info.name, i);
                        layerInfoList.Add(info);
                    }
                    break;
                }
            }
        }

        private void InitWindowSize()
        {
            float width = NameFieldWidth + layerInfoList.Count * CheckBoxWidth;
            minSize = maxSize = new Vector2(width, 500);
            generateButtonRect = new Rect(width - generateButtonOffset.x, 500 - generateButtonOffset.y, generateButtonSize.x, generateButtonSize.y);
        }

        private void LoadCache()
        {
            string filePath = Application.dataPath + LayerCacheFilePath;
            filePath = filePath.Replace("/", "\\");
            if (!File.Exists(filePath))
            {
                return;
            }
            StreamReader sr = new StreamReader(filePath);
            string str = "";
            while (!string.IsNullOrEmpty(str = sr.ReadLine()))
            {
                string[] argv = str.Trim().Split(' ');
                MaskCell cell = new MaskCell();
                cell.name = argv[0];
                cell.flags = new bool[layerInfoList.Count];
                for (int i = 1; i < argv.Length; ++i)
                {
                    if (layerInfoIndexCache.ContainsKey(argv[i]))
                    {
                        cell.flags[layerInfoIndexCache[argv[i]]] = true;
                    }
                }
                maskCellList.Add(cell);
            }
            sr.Close();
        }

        private void OnMaskCellHeaderDraw(Rect rect)
        {
            Rect editingRect = rect;
            editingRect.x = 0f;
            editingRect.width = NameFieldWidth;
            EditorGUI.LabelField(editingRect, "名字");
            for (int i = 0; i < layerInfoList.Count; ++i)
            {
                editingRect = rect;
                editingRect.x = NameFieldWidth + i * CheckBoxWidth;
                editingRect.width = CheckBoxWidth;
                EditorGUI.LabelField(editingRect, layerInfoList[i].name);
            }
        }

        private void OnMaskCellElementDraw(Rect rect, int index, bool isactive, bool isfocused)
        {
            Rect editingRect = rect;
            editingRect.x = 0f;
            editingRect.width = NameFieldWidth;
            maskCellList[index].name = EditorGUI.TextField(editingRect, maskCellList[index].name);
            for (int i = 0; i < maskCellList[index].flags.Length; ++i)
            {
                editingRect = rect;
                editingRect.x = NameFieldWidth + i * CheckBoxWidth;
                editingRect.width = editingRect.height;
                maskCellList[index].flags[i] = EditorGUI.Toggle(editingRect, maskCellList[index].flags[i]);
            }
        }

        private void OnMaskCellAdd(ReorderableList list)
        {
            MaskCell cell = new MaskCell();
            cell.name = "NewCell";
            cell.flags = new bool[layerInfoList.Count];
            maskCellList.Add(cell);
        }

        private void OnMaskCellRemove(ReorderableList list)
        {
            maskCellList.RemoveAt(list.index);
        }

        private void Generate()
        {
            GenerateCache();
            GenerateCode();
            Debug.LogError("Generate Complete!!");
        }

        private void GenerateCache()
        {
            string filePath = Application.dataPath + LayerCacheFilePath;
            string str = "";
            for (int i = 0; i < maskCellList.Count; ++i)
            {
                str += maskCellList[i].name + " ";
                for (int j = 0; j < layerInfoList.Count; ++j)
                {
                    if (maskCellList[i].flags[j])
                    {
                        str += layerInfoList[j].name + " ";
                    }
                }
                str += "\r\n";
            }
            FileStream fs = new FileStream(filePath, FileMode.Create);
            byte[] buffers = Encoding.Default.GetBytes(str);
            fs.Write(buffers, 0, buffers.Length);
            fs.Flush();
            fs.Close();
        }

        private void GenerateCode()
        {
            stringBuilder.Remove(0, stringBuilder.Length);
            tabIndex = 0;
            Tab().Append("using System.Collections.Generic;\r\n\r\n");
            Tab().Append("public enum ELayerMaskType \r\n");
            Tab().Append("{\r\n");
            tabIndex++;
            for (int i = 0; i < maskCellList.Count; ++i)
            {
                Tab().Append(maskCellList[i].name).Append(",\r\n");
            }
            Tab().Append("Count\r\n");
            tabIndex--;
            Tab().Append("}\r\n");
            Tab().Append("\r\n");
            Tab().Append("public static partial class LayerUtility \r\n");
            Tab().Append("{\r\n");
            tabIndex++;
            Tab().Append("private static List<int> layerMaskList = new List<int>() \r\n");
            Tab().Append("{\r\n");
            tabIndex++;
            for (int i = 0; i < maskCellList.Count; ++i)
            {

                int mask = 0;
                string str = "";
                for (int j = 0; j < layerInfoList.Count; ++j)
                {
                    if (maskCellList[i].flags[j])
                    {
                        mask |= 1 << layerInfoList[j].id;
                        str += layerInfoList[j].name + " ";
                    }
                }
                Tab().Append(mask).Append(", \t//").Append(str).Append("\r\n");
            }
            tabIndex--;
            Tab().Append("};\r\n");
            tabIndex--;
            Tab().Append("};\r\n");

            string filePath = Application.dataPath + GenerateFilePath;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            FileStream fs = new FileStream(filePath, FileMode.Create);
            byte[] buffers = Encoding.Default.GetBytes(stringBuilder.ToString());
            fs.Write(buffers, 0, buffers.Length);
            fs.Flush();
            fs.Close();
        }

        private StringBuilder Tab()
        {
            for (int i = 0; i < tabIndex; ++i)
            {
                stringBuilder.Append("\t");
            }
            return stringBuilder;
        }

        private class LayerInfo
        {
            public int id;
            public string name;
        }

        private class MaskCell
        {
            public string name;
            public bool[] flags;
        }
    }
}
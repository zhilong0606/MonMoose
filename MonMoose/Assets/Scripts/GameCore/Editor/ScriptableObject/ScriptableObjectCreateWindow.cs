using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using MonMoose.Core;

namespace MonMoose.Core
{
    public class ScriptableObjectCreateWindow : EditorWindow
    {
        //private const string m_outputPath = "Assets/OutPut/ScriptableObject/{0}.asset";

        private SortedDictionary<string, Type> m_typeMap = new SortedDictionary<string, Type>();
        private string m_searchStr = string.Empty;
        private Vector2 m_scrollPos;
        private bool m_isCreating;

        [MenuItem("Assets/CommonTools/Creat Setting File", false, 2)]
        static void Creat()
        {
            ScriptableObjectCreateWindow window = (ScriptableObjectCreateWindow)GetWindow(typeof(ScriptableObjectCreateWindow), false, "ScriptableObject Creater");
            window.autoRepaintOnSceneChange = true;
            window.Show();
        }

        private void Awake()
        {
            Refresh();
        }

        private void Refresh()
        {
            m_typeMap.Clear();
            Type baseType = typeof(BaseScriptableObject);
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type[] types = assembly.GetTypes();
                for (int i = 0; i < types.Length; ++i)
                {
                    Type type = types[i];
                    if (type.IsAbstract)
                    {
                        continue;
                    }
                    if (baseType.IsAssignableFrom(type) && type != baseType)
                    {
                        m_typeMap.Add(type.Name, type);
                    }
                }
            }
        }

        private bool TryGetFilePath(string typeName, out string filePath, out string errorLog)
        {
            filePath = string.Empty;
            errorLog = string.Empty;
            var selectedObjs = Selection.objects;
            if (selectedObjs.Length != 1)
            {
                errorLog = "请选中一个文件夹";
                return false;
            }
            var selectedObj = selectedObjs[0];
            string selectedPath = AssetDatabase.GetAssetPath(selectedObj);
            if (!Directory.Exists(selectedPath))
            {
                selectedPath = AssetPathUtility.GetFileFolderPath(selectedPath);
                if (!Directory.Exists(selectedPath))
                {
                    errorLog = "请选中一个文件夹";
                    return false;
                }
            }
            filePath = string.Format("{0}/{1}.asset", selectedPath, typeName);
            int index = 1;
            while (File.Exists(filePath))
            {
                filePath = string.Format("{0}/{1}{2}.asset", selectedPath, typeName, index);
                index++;
            }
            return true;
        }

        private void Create(string typeName)
        {
            m_isCreating = true;
            string errorLog;
            string filePath;
            if (TryGetFilePath(typeName, out filePath, out errorLog))
            {
                Type type;
                if (m_typeMap.TryGetValue(typeName, out type))
                {
                    ScriptableObject obj = Activator.CreateInstance(type) as ScriptableObject;
                    if (obj != null && !File.Exists(filePath))
                    {
                        AssetDatabase.CreateAsset(obj, filePath);
                    }
                }
            }
            else
            {
                ShowNotification(new GUIContent(errorLog));
            }
            m_isCreating = false;
        }

        private void OnGUI()
        {
            DrawTitle();
            DrawSeparator();
            DrawSearchTextField();
            DrawTypes();
        }

        private void DrawTitle()
        {
            GUILayout.Label("Showing all ScriptableObejct inherited from BaseScriptableObject");
        }

        private void DrawSeparator()
        {
            GUILayout.Space(12f);

            if (Event.current.type == EventType.Repaint)
            {
                Texture2D tex = EditorGUIUtility.whiteTexture;
                Rect rect = GUILayoutUtility.GetLastRect();
                GUI.color = new Color(0f, 0f, 0f, 0.25f);
                GUI.DrawTexture(new Rect(0f, rect.yMin + 6f, Screen.width, 4f), tex);
                GUI.DrawTexture(new Rect(0f, rect.yMin + 6f, Screen.width, 1f), tex);
                GUI.DrawTexture(new Rect(0f, rect.yMin + 9f, Screen.width, 1f), tex);
                GUI.color = Color.white;
            }
        }

        private void DrawSearchTextField()
        {
            EditorGUILayout.BeginHorizontal();
            GUI.SetNextControlName("SearchTextField");
            m_searchStr = EditorGUILayout.TextField("", m_searchStr, "SearchTextField");
            if (GUILayout.Button("", "SearchCancelButton", GUILayout.Width(18f)))
            {
                m_searchStr = "";
                GUIUtility.keyboardControl = 0;
            }
            if (Event.current.Equals(Event.KeyboardEvent("return")))
            {
                if (GUI.GetNameOfFocusedControl() != "SearchTextField")
                {
                    GUI.FocusControl("SearchTextField");
                }
            }
            if (GUILayout.Button("Refresh", GUILayout.Width(120f)))
            {
                Refresh();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawTypes()
        {
            m_scrollPos = EditorGUILayout.BeginScrollView(m_scrollPos);
            string[] filters = m_searchStr.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var kv in m_typeMap)
            {
                string typeName = kv.Key;
                if (!IsMatchFilters(typeName, filters))
                {
                    continue;
                }
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(typeName, EditorStyles.textField);
                if (!m_isCreating && GUILayout.Button("Creat", GUILayout.Width(120f)))
                {
                    Create(typeName);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
        }

        private bool IsMatchFilters(string str, string[] filters)
        {
            for (int j = 0; j < filters.Length; ++j)
            {
                if (str.IndexOf(filters[j], StringComparison.OrdinalIgnoreCase) == -1)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
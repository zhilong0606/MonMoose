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
        private const string m_outputPath = "Assets/OutPut/ScriptableObject/{0}.asset";

        private Dictionary<string, Type> m_typeMap = new Dictionary<string, Type>();
        private List<string> m_fileNameList = new List<string>();
        private string m_searchStr = string.Empty;
        private Vector2 m_scrollPos;
        private bool m_isCreating;

        [MenuItem("Setting File/Creat")]
        static void Creat()
        {
            //ScriptableObjectCreateWindow settingSelector = DisplayWizard<ScriptableObjectCreateWindow>("Choose Settings");
            //settingSelector.ShowTab();
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
            Type[] types = baseType.Assembly.GetTypes();
            for (int i = 0; i < types.Length; ++i)
            {
                Type type = types[i];
                if (baseType.IsAssignableFrom(type) && type != baseType)
                {
                    m_typeMap.Add(type.Name, type);
                }
            }
        }

        private void CollectFileNames()
        {
            m_fileNameList.Clear();
            string folderPath = Path.GetDirectoryName(m_outputPath);
            DirectoryInfo dirInfo = new DirectoryInfo(folderPath);
            if (dirInfo.Exists)
            {
                FileInfo[] files = dirInfo.GetFiles("*.asset");
                for (int i = 0; i < files.Length; ++i)
                {
                    string fileName = files[i].Name;
                    int index = fileName.IndexOf(".asset");
                    fileName = fileName.Substring(0, index);
                    m_fileNameList.Add(fileName);
                }
            }
        }

        private void Create(string typeName)
        {
            m_isCreating = true;
            string filePath = string.Format(m_outputPath, typeName);
            string folderPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                AssetDatabase.ImportAsset(folderPath);
            }
            Type type = m_typeMap.GetClassValue(typeName);
            if (type != null)
            {
                ScriptableObject obj = Activator.CreateInstance(type) as ScriptableObject;
                if (obj != null && !File.Exists(filePath))
                {
                    AssetDatabase.CreateAsset(obj, filePath);
                }
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
                CollectFileNames();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawTypes()
        {
            CollectFileNames();
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
                if (m_fileNameList.Contains(typeName))
                {
                    GUILayout.Space(120f);
                }
                else if (!m_isCreating && GUILayout.Button("Creat", GUILayout.Width(120f)))
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
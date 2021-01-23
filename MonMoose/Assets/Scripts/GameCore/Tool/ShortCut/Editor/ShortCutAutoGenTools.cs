using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MonMoose.Core;
using UnityEditor;
using UnityEngine;

public class ShortCutAutoGenTools : Editor
{
    [MenuItem("Tools/ShortCut/Generate Code")]
    public static void GenerateCode()
    {
        ShortCutGenerator g = new ShortCutGenerator();
        g.Generate(OnGetClassPath, OnLog);
    }

    private static string OnGetClassPath(Type type)
    {
        string path = AssetDatabase.FindAssets("t:Script")
            .Where(v => Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath(v)) == type.Name)
            .Select(id => AssetDatabase.GUIDToAssetPath(id))
            .FirstOrDefault();
        return AssetPathUtility.Concat(Application.dataPath, path, "Assets");
    }

    private static void OnLog(string logStr)
    {
        Debug.LogError(logStr);
    }
}

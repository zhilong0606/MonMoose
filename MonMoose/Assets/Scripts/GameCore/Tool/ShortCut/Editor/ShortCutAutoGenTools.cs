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
        g.Generate(AssetPathUtility.GetClassFullPath, OnLog);
    }

    private static void OnLog(string logStr)
    {
        Debug.LogError(logStr);
    }
}

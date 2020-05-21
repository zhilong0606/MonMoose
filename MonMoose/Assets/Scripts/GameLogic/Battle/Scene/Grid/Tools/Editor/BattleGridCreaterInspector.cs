using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MonMoose.Logic
{
    [CustomEditor(typeof(BattleGridCreater))]
    public class BattleGridCreaterInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Create"))
            {
                (target as BattleGridCreater).Create();
            }
        }
    }
}

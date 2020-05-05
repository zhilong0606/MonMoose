using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using MonMoose.Core;
using UnityEngine;

public class UITestInitializer : MonoBehaviour
{
    public EWindowType[] types = new EWindowType[0];

    private void Awake()
    {
        GameInitializer initializer = new GameInitializer();
        initializer.StartSync();
        for (int i = 0; i < types.Length; ++i)
        {
            UIWindowManager.instance.OpenWindow((int)types[i]);
        }
    }

    private void Update()
    {
        TickManager.instance.Tick(Time.deltaTime);
    }
}

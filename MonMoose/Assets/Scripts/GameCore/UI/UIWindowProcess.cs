using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowProcess
{
    public Action OnEndCb;
    private bool isStart = false;

    public bool IsStart
    {
        get { return isStart; }
    }

    public void UpdateLogic(float deltaTime)
    {

    }

    public void Start()
    {
        isStart = true;
        OnStart();
    }
    
    public void End()
    {
        isStart = false;
        OnEnd();
        if (OnEndCb != null)
        {
            OnEndCb();
        }
    }

    protected virtual void OnStart()
    {
    }

    protected virtual void OnEnd()
    {
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIProcess
{
    bool needSkip { get; }
    void StartProcess();
}

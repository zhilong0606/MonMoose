using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Core
{
    public interface IUIProcess
    {
        bool needSkip { get; }
        void StartProcess();
    }
}

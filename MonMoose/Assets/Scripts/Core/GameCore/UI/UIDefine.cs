using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Core
{
    public delegate void WindowFlagDelegate(UIWindow window, bool flag);

    public delegate void BoolValueChangeDelegate(bool value, bool isNew);

    public delegate void IntValueChangeDelegate(int value, bool isNew);

    public delegate void ValueChangeDelegate<T>(T value, bool isNew);


    public enum ETextureLayout
    {
        None,
        Horizonatal,
        Vertical
    }

    public enum EInputState
    {
        Up,
        Down,
    }

    public enum EClickProcess
    {
        None,
        FirstDown,
        FirstUp,
        TwiceDown,
        TwiceUp,
    }

    public enum EClickTriggerType
    {
        Down,
        Up,
    }

    public class UIDefine
    {
    }
}

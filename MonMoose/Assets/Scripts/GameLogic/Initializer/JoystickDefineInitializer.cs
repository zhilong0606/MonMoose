using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

public enum EJoystickType
{
    Move,
    Skill,
    Count,
}

public class JoystickDefineInitializer : Initializer
{
    protected override IEnumerator OnProcess()
    {
        JoystickManager.instance.CreateContext((int)EJoystickType.Move);
        JoystickManager.instance.CreateContext((int)EJoystickType.Skill);
        yield return null;
    }
}

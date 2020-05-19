using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

public class GameInitState : State
{
    public override int stateIndex
    {
        get { return (int)EGameState.GameInit; }
    }

    public override void OnEnter()
    {
        UIWindowManager.instance.OpenWindow((int)EWindowType.InitWindow);
    }

    public override void OnExit()
    {
        UIWindowManager.instance.DestroyAllWindow();
    }
}

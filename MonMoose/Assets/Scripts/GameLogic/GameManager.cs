using System;
using System.Collections;
using MonMoose.Core;
using UnityEngine;

public enum EGameState
{
    GameInit,
    Lobby,
    Battle,
}

public class GameManager : MonoSingleton<GameManager>
{
    private GameInitializer m_initializer = new GameInitializer();
    private StateMachine stateMachine;

    protected override void Init()
    {
        stateMachine = new StateMachine(new State[]
        {
            new LobbyState(),
            new BattleState(),
        });
        RegisterListener();
        m_initializer.StartAsync(OnInitFinish);
    }

    private void OnInitFinish()
    {
        stateMachine.ChangeState((int)EGameState.Lobby);
    }

    private void RegisterListener()
    {
    }

    private void RemoveListener()
    {
    }

    public void EnterBattle()
    {
        stateMachine.ChangeState((int)EGameState.Battle);
    }

    private void Update()
    {
        TickManager.instance.Tick(Time.deltaTime);
        stateMachine.TickFloat(Time.deltaTime);
    }

    protected override void UnInit()
    {
        RemoveListener();
        base.UnInit();
    }
}


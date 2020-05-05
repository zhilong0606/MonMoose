using System;
using System.Collections;
using MonMoose.Core;
using UnityEngine;

public enum EGameState
{
    Lobby,
    SkillTest,
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
            new SkillTestState(),
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
        EventManager.instance.RegisterListener((int)EventID.LobbyWindow_SkillTestBtn_Click, OnSkillTestBtnClick);
    }

    private void RemoveListener()
    {
        EventManager.instance.UnregisterListener((int)EventID.LobbyWindow_SkillTestBtn_Click, OnSkillTestBtnClick);
    }

    private void OnSkillTestBtnClick()
    {
        stateMachine.ChangeState((int)EGameState.SkillTest);
    }

    private void Update()
    {
        TickManager.instance.Tick(Time.deltaTime);
    }

    protected override void UnInit()
    {
        RemoveListener();
        base.UnInit();
    }
}


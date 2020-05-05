using MonMoose.Core;
using UnityEngine.SceneManagement;

public class SkillTestState : State
{
    private BattleSystem battleSystem = new BattleSystem();

    public override int stateIndex
    {
        get { return (int)EGameState.SkillTest; }
    }

    public override void OnEnter()
    {
        StartFightHandler.CreateInstance();
        PlayerManager.CreateInstance();
        ActorManager.CreateInstance();
        SceneManager.LoadScene("SkillTest");
        RegisterListener();
    }

    public override void OnExit()
    {
        RemoveListener();
        battleSystem.Clear();
        battleSystem = null;
        StartFightHandler.DestroyInstance();
        PlayerManager.DestroyInstance();
        ActorManager.DestroyInstance();
    }

    private void RegisterListener()
    {
        EventManager.instance.RegisterListener((int)EventID.Frame_Tick, OnFrameTick);
        EventManager.instance.RegisterListener((int)EventID.Actor_All_Initialized, OnActorAllInitialized);
    }

    private void RemoveListener()
    {
        EventManager.instance.UnregisterListener((int)EventID.Frame_Tick, OnFrameTick);
        EventManager.instance.UnregisterListener((int)EventID.Actor_All_Initialized, OnActorAllInitialized);
    }

    private void OnFrameTick()
    {
        battleSystem.UpdateLogic();
        ActorManager.instance.UpdateLogic();
    }

    private void OnActorAllInitialized()
    {
        battleSystem.Init();
    }
}

using MonMoose.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleState : State
{
    private BattleSystem battleSystem = new BattleSystem();

    public override int stateIndex
    {
        get { return (int)EGameState.Battle; }
    }

    public override void OnEnter()
    {
        StartFightHandler.CreateInstance();
        PlayerManager.CreateInstance();
        ActorManager.CreateInstance();
        SceneManager.LoadSceneAsync("BattleScene");
        SceneManager.sceneLoaded += OnSceneLoadCompleted;
        RegisterListener();
    }

    private void OnSceneLoadCompleted(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= OnSceneLoadCompleted;
        InitScene();
    }


    public override void OnExit()
    {
        RemoveListener();
        battleSystem.Clear();
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

    private void InitScene()
    {
        GameObject sceneConfigRoot = GameObject.Find("SceneConfigRoot");
        if (sceneConfigRoot == null)
        {
            return;
        }
        BattleSceneConfig sceneConfig = sceneConfigRoot.GetComponent<BattleSceneConfig>();
        if (sceneConfig == null)
        {
            return;
        }
        BattleGridView[] gridViews = sceneConfig.gridRoot.GetComponentsInChildren<BattleGridView>();
        for (int i = 0; i < gridViews.Length; ++i)
        {
            BattleGrid grid = new BattleGrid();
            grid.Init(gridViews[i], gridViews[i].position);
            BattleGridManager.instance.AddGrid(grid);
        }
        GameObject go = ResourceManager.instance.GetPrefab("Exported/Actor/001/Prefabs/Actor");
        GameObject actorObj = GameObject.Instantiate<GameObject>(go);
        actorObj.transform.position = BattleGridManager.instance.GetGrid(0, 0).transPos;
    }

    private BattleGrid m_downGrid;

    public override void OnTickFloat(float deltaTime)
    {
        base.OnTickFloat(deltaTime);
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, LayerMask.GetMask("BattleGrid")))
            {
                BattleGridView view = hit.transform.GetComponent<BattleGridView>();
                if (view != null)
                {
                    m_downGrid = BattleGridManager.instance.GetGrid(view.position);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, LayerMask.GetMask("BattleGrid")))
            {
                BattleGridView view = hit.transform.GetComponent<BattleGridView>();
                if (view != null)
                {

                }
            }
        }
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

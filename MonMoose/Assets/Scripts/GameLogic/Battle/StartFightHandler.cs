using MonMoose.Core;
using UnityEngine;

namespace MonMoose.Logic
{
    public class StartFightHandler : Singleton<StartFightHandler>
    {
        //private SceneObjInventory sceneInventory;

        //protected override void Init()
        //{
        //    base.Init();
        //    RegisterListener();
        //    Test();
        //}

        //protected override void UnInit()
        //{
        //    base.UnInit();
        //    RemoveListener();
        //}

        //private void RegisterListener()
        //{
        //    EventManager.instance.RegisterListener((int)EventID.FightScene_Awake, OnFightSceneAwake);
        //}

        //private void RemoveListener()
        //{
        //    EventManager.instance.UnregisterListener((int)EventID.FightScene_Awake, OnFightSceneAwake);
        //}

        //private void OnFightSceneAwake()
        //{
        //    sceneInventory = GameObject.Find("SceneRoot").GetComponent<SceneObjInventory>();
        //    PlayerManager.instance.Ergodic(OnPlayerInit);
        //    EventManager.instance.Broadcast((int)EventID.Actor_All_Initialized);
        //    UIWindowManager.instance.OpenWindow((int)EWindowId.JoystickWindow);
        //}

        //private void OnPlayerInit(Player player)
        //{
        //    ActorInfo actorInfo = GameDataManager.instance.actorInfoDic[player.actorID];
        //    GameObject prefab = ResourceManager.instance.GetResource(actorInfo.prefabPath) as GameObject;
        //    Actor actor = Object.Instantiate(prefab).GetComponent<Actor>();
        //    actor.Init(player.actorID);
        //    actor.Born(sceneInventory.regions[0]);
        //    actor.camp = player.camp;
        //    player.selectedActor = actor;
        //    actor.ownerID = player.playerID;
        //    ActorManager.instance.AddHero(actor);
        //    FrameSyncManager.instance.Start();
        //}

        //private void Test()
        //{
        //    Player player = new Player();
        //    player.playerID = 110;
        //    player.isAI = false;
        //    player.camp = ECampType.Camp1;
        //    player.actorID = 101;
        //    PlayerManager.instance.AddPlayer(player, true);
        //    player = new Player();
        //    player.playerID = 120;
        //    player.isAI = false;
        //    player.camp = ECampType.Camp2;
        //    player.actorID = 101;
        //    PlayerManager.instance.AddPlayer(player, false);
        //}
    }
}

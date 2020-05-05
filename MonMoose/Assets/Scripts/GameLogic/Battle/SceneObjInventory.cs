using MonMoose.Core;
using UnityEngine;

public class SceneObjInventory : MonoBehaviour
{
    public SpawnRegion[] regions;

    private void Awake()
    {
        EventManager.instance.Broadcast((int)EventID.FightScene_Awake);
    }
}

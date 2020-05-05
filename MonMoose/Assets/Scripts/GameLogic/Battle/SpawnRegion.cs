using UnityEngine;

public class SpawnRegion : MonoBehaviour
{
    [SerializeField]
    private int posX, posY, posZ;

    public ECampType camp;

    [SerializeField]
    private int rotX, rotY, rotZ;

    public FixVec3 Position
    {
        get { return new FixVec3(posX, posY, posZ) / FrameSyncDefine.Precision; }   
    }
    public FixVec3 Rotation
    {
        get { return new FixVec3(rotX, rotY, rotZ) / FrameSyncDefine.Precision; }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            posX = (int)(transform.position.x * FrameSyncDefine.Precision);
            posY = (int)(transform.position.y * FrameSyncDefine.Precision);
            posZ = (int)(transform.position.z * FrameSyncDefine.Precision);
            rotX = (int)(transform.eulerAngles.x * FrameSyncDefine.Precision);
            rotY = (int)(transform.eulerAngles.y * FrameSyncDefine.Precision);
            rotZ = (int)(transform.eulerAngles.z * FrameSyncDefine.Precision);
        }
    }
#endif
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneConfig : MonoBehaviour
{
    [SerializeField]
    private GameObject m_gridRoot;
    public GameObject gridRoot { get { return m_gridRoot; } }

    [SerializeField]
    private GameObject m_actorRoot;
    public GameObject actorRoot { get { return m_actorRoot; } }
}

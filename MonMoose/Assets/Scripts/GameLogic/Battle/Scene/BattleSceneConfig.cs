using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.GameLogic.Battle
{
    public class BattleSceneConfig : MonoBehaviour
    {
        [SerializeField] private GameObject m_gridRoot;

        public GameObject gridRoot
        {
            get { return m_gridRoot; }
        }
    }
}

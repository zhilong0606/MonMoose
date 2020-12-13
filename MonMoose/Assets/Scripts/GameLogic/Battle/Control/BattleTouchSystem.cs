using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.GameLogic;
using UnityEngine;

namespace MonMoose.GameLogic.Battle
{
    public class BattleTouchSystem : Singleton<BattleTouchSystem>
    {
        public BattleGridView GetGridView(Vector2 screenPos)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPos);

            BattleGridView gridView = null;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, LayerUtility.GetLayerMask(ELayerMaskType.Grid)))
            {
                gridView = hit.transform.GetComponent<BattleGridView>();
            }
            return gridView;
        }
    }
}

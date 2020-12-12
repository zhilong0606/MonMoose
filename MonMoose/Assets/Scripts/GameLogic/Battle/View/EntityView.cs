using System.Collections;
using System.Collections.Generic;
using MonMoose.Battle;
using MonMoose.Core;
using UnityEngine;

namespace MonMoose.GameLogic.Battle
{
    public abstract class EntityView : BattleView
    {
        protected StaticLerpVec3 posLerp = new StaticLerpVec3();

        public void SetPosition(BattleGrid grid, DcmVec2 offset, bool isTeleport)
        {
            Vector3 worldPos = BattleGridManager.instance.GetWorldPosition(grid.gridPosition, offset);
            if (isTeleport)
            {
                transform.position = worldPos;
                posLerp.Stop();
            }
            else
            {
                posLerp.Ready(transform.position, worldPos, 0.1f);
                posLerp.Start();
            }
        }

        public void SetForward(DcmVec2 forward)
        {
            rotateRoot.transform.forward = new Vector3((float)forward.x, 0f, (float)forward.y);
        }
    }
}
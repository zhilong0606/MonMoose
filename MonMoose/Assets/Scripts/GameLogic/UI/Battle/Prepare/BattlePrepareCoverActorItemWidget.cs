using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.GameLogic.UI
{
    public class BattlePrepareCoverActorItemWidget : BattlePrepareActorItemBaseWidget
    {

        public void SetScreenPos(Vector2 screenPos)
        {
            Vector2 localPos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, screenPos, canvas.camera, out localPos))
            {
                (transform as RectTransform).anchoredPosition = localPos;
            }
        }
    }
}

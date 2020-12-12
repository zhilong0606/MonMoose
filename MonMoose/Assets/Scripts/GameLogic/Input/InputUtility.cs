using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.GameLogic
{
    public static class InputUtility
    {
        public static Vector2 GetTouchScreenPos(int fingerId)
        {
#if UNITY_EDITOR
            return Input.mousePosition;
#else
            for (int i = 0; i < Input.touchCount; ++i)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.fingerId == fingerId)
                {
                    return touch.position;
                }
            }
#endif
        }
    }
}

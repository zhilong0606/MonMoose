using UnityEngine;
using UnityEngine.UI;

public class UITouchArea : Image
{
    public override bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, sp, eventCamera))
        {
            return true;
        }
        return false;
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        return;
    }
}

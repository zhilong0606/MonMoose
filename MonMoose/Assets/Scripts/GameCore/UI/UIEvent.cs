using MonMoose.Core;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEvent : ClassPoolObj
{
    public GameObject widget;
    public PointerEventData eventData;
    public object extraParam;

    public void Recycle()
    {
        widget = null;
        extraParam = null;
        eventData = null;
    }
}


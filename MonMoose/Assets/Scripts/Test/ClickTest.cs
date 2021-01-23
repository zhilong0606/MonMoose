using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickTest : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject inputObj = GetInputObj<IPointerClickHandler>(eventData.pointerCurrentRaycast.gameObject);
        Debug.LogError(inputObj.name);
        //Debug.LogError(eventData.selectedObject.name);
    }

    private GameObject GetInputObj<T>(GameObject go)
    {
        if (go == null)
        {
            return null;
        }
        Transform trans = go.transform;
        while (trans != null)
        {
            T t = trans.GetComponent<T>();
            if (t != null)
            {
                return trans.gameObject;
            }
            trans = trans.parent;
        }
        return null;
    }
}

using UnityEngine;
using System.Collections;

public class LocalizationWidget : UIComponent
{
    public bool skip = false;
    [SerializeField]
    protected int id;
    protected int language;

    public override bool needAutoInit
    {
        get { return true; }
    }

    public int Language
    {
        set
        {
            language = value;
            OnLanguageUpdate();
        }
    }

    public int Id
    {
        get { return id; }
        set
        {
            id = value;
            OnIdUpdate();
        }
    }

    public virtual void OnLanguageUpdate()
    {

    }

    public virtual void OnIdUpdate()
    {

    }
}

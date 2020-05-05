using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeStaticInfo : BaseGameInfo
{
    public int id;
    public string nameStr;
    public bool isSignInversed;
    public string iconPath;

    public override void LoadCSVLine(string lineStr)
    {
        string[] attrStrs = lineStr.Split(',');
        id = int.Parse(attrStrs[0]);
        nameStr = attrStrs[1];
        isSignInversed = bool.Parse(attrStrs[2]);
        iconPath = attrStrs[3];
    }
}

using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager>
{
    public Dictionary<int, ActorInfo> actorInfoDic = new Dictionary<int, ActorInfo>();
    public Dictionary<int, SkillInfo> skillInfoDic = new Dictionary<int, SkillInfo>();
    public Dictionary<int, AnimationInfo> animationInfoDic = new Dictionary<int, AnimationInfo>();
    public Dictionary<int, AttributeStaticInfo> attributeStaticInfoMap = new Dictionary<int, AttributeStaticInfo>();

    protected override void Init()
    {
        //LoadCSV(actorInfoDic, "Exporter/GameData/ActorInfo");
        //LoadCSV(skillInfoDic, "Exporter/GameData/SkillInfo");
        //LoadCSV(animationInfoDic, "Exporter/GameData/AnimationInfo");
        //LoadCSV(attributeStaticInfoMap, "Exporter/GameData/AttributeStaticInfo");
    }

    private void LoadCSV<T>(Dictionary<int,T> dic, string csvPath) where T : BaseGameInfo, new()
    {
        TextAsset text = Resources.Load(csvPath) as TextAsset;
        string[] strs = text.text.Split('\n');
        for (int i = 1; i < strs.Length; ++i)
        {
            if (string.IsNullOrEmpty(strs[i]))
            {
                continue;
            }
            T info = new T();
            info.LoadCSVLine(strs[i].Trim());
            dic.Add(info.id, info);
        }
    }
}

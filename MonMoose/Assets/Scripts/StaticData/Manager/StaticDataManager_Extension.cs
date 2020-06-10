using System.Collections;
using System.Collections.Generic;

namespace MonMoose.StaticData
{
    public partial class StaticDataManager
    {
        private Dictionary<int, EntityStaticInfo> m_actorToEntityMap = new Dictionary<int, EntityStaticInfo>();
        
        partial void OnLoadAllEnd()
        {
            AnalyzeEntityStaticInfo();
        }

        private void AnalyzeEntityStaticInfo()
        {
            for (int i = 0; i < m_EntityList.Count; ++i)
            {
                m_actorToEntityMap.Add(m_EntityList[i].SpecificId, m_EntityList[i]);
            }
        }

        public EntityStaticInfo GetEntityInfo(EEntityType type, int specificId)
        {
            EntityStaticInfo staticInfo = null;
            Dictionary<int, EntityStaticInfo> specificMap = null;
            switch (type)
            {
                case EEntityType.Actor:
                    specificMap = m_actorToEntityMap;
                    break;
            }
            if (specificMap != null)
            {
                specificMap.TryGetValue(specificId, out staticInfo);
            }
            return staticInfo;
        }
    }
}

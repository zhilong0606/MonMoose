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
            for (int i = 0; i < m_entityList.Count; ++i)
            {
                m_actorToEntityMap.Add(m_entityList[i].SpecificId, m_entityList[i]);
            }
        }

        public EntityStaticInfo GetEntity(EEntityType type, int specificId)
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

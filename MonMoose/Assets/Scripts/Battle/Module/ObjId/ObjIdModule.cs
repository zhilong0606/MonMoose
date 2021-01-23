using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;

namespace MonMoose.Battle
{
    public class ObjIdModule : Module
    {
        private Dictionary<int, int> m_idMap = new Dictionary<int, int>();
        private const int m_capacity = 10000;

        [ShortCutMethod(false)]
        public int CreateObjId(EBattleObjType type)
        {
            int typeInt = (int)type;
            int id;
            if (!m_idMap.TryGetValue(typeInt, out id))
            {
                m_idMap.Add(typeInt, 0);
            }
            id++;
            if (id >= m_capacity)
            {
                Debug.LogError(m_battleInstance, "[ObjIdModule] {0} is Out of Range", type);
            }
            m_idMap[typeInt] = id;
            return m_capacity * typeInt + id;
        }
    }
}

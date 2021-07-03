using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Battle
{
    public class EntityDataComponent : EntityComponent
    {
        public override EEntityComponentType type
        {
            get { return EEntityComponentType.Data; }
        }

        private List<EntityData> m_dataList = new List<EntityData>();

        public T CreateData<T>() where T : EntityData
        {
            T data = m_battleInstance.FetchPoolObj<T>(this);
            AddData(data);
            return data;
        }

        public T GetData<T>() where T : EntityData
        {
            for (int i = 0; i < m_dataList.Count; ++i)
            {
                T data = m_dataList[i] as T;
                if (data != null)
                {
                    return data;
                }
            }
            return null;
        }

        public void AddData(EntityData data)
        {
            m_dataList.Add(data);
        }
    }
}

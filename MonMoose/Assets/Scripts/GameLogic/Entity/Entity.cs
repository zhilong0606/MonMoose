using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    protected EntityView m_view;
    public EntityView view { get { return m_view; } }

    protected List<EntityComponent> m_componentList = new List<EntityComponent>();

    public void Tick(Fix32 deltaTime)
    {
        for (int i = 0; i < m_componentList.Count; ++i)
        {
            m_componentList[i].Tick(deltaTime);
        }
    }
}

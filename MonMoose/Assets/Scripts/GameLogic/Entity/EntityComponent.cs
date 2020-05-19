using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityComponent
{
    protected Entity m_entity;

    public void Tick(Fix32 deltaTime)
    {
        OnTick(deltaTime);
    }

    protected virtual void OnTick(Fix32 deltaTime)
    {
        
    }
}

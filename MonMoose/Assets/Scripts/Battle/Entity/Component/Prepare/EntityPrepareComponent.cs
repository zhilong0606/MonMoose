using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public class EntityPrepareComponent : EntityComponent
    {
        public override EEntityComponentType type
        {
            get { return EEntityComponentType.Prepare; }
        }

        
    }
}

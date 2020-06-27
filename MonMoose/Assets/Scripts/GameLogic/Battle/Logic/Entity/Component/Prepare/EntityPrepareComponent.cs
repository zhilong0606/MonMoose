using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class EntityPrepareComponent : EntityComponent
    {
        public override EEntityComponentType type
        {
            get { return EEntityComponentType.Prepare; }
        }

        
    }
}

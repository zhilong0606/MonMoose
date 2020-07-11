using System.Collections;
using System.Collections.Generic;

namespace MonMoose.BattleLogic
{
    public class EntityPrepareComponent : EntityComponent
    {
        public override EEntityComponentType type
        {
            get { return EEntityComponentType.Prepare; }
        }

        
    }
}

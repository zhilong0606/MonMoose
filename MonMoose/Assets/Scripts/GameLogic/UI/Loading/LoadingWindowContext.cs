using System;
using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

namespace MonMoose.GameLogic.UI
{
    public class LoadingWindowContext : ClassPoolObj
    {
        public ELoadingId id;
        public ELoadingWindowType windowType;
        public Action actionOnEnd;

        public override void OnRelease()
        {
            base.OnRelease();
            actionOnEnd = null;
        }
    }
}

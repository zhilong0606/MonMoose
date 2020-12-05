﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Battle
{
    public class FrameSyncRelayLocal : FrameSyncRelay
    {
        public override void Send(FrameCommandWrap cmd)
        {
            Receive(cmd);
        }

        public override void Send(FrameCut cut)
        {
            Receive(cut);
        }
    }
}

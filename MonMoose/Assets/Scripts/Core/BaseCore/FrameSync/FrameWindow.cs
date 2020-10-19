using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Core
{
    public class FrameWindow
    {
        private int m_cmdTypeCount;

        private List<FrameCut> m_frameCutList = new List<FrameCut>(64);

        private ClassPool<FrameCut> m_frameCutPool = new ClassPool<FrameCut>();
        
        public void Init()
        {

        }

        private FrameCut CreateFrameCut()
        {
            FrameCut frameCut = m_frameCutPool.Fetch();
            frameCut.Init(m_cmdTypeCount);
            return frameCut;
        }
    }
}

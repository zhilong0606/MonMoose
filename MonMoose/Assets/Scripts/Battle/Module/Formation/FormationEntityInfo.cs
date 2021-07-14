using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;

namespace MonMoose.Battle
{
    public class FormationEntityInfo : BattleObj
    {
        public EntityInitData data;
        public int count;
        public List<FormationEntitySetupInfo> setupList = new List<FormationEntitySetupInfo>();

        public bool canSetup
        {
            get { return setupList.Count < count; }
        }

        public bool Setup(int uid, GridPosition gridPosition)
        {
            if (!canSetup)
            {
                return false;
            }
            FormationEntitySetupInfo setupInfo = m_battleInstance.FetchPoolObj<FormationEntitySetupInfo>(this);
            setupInfo.uid = uid;
            setupInfo.gridPosition = gridPosition;
            return true;
        }

        public bool Retreat(int uid)
        {
            FormationEntitySetupInfo setupInfo = GetSetupInfo(uid);
            if (setupInfo == null)
            {
                return false;
            }
            setupList.Remove(setupInfo);
            setupInfo.Release();
            return true;
        }

        public FormationEntitySetupInfo GetSetupInfo(int uid)
        {
            foreach (var setupInfo in setupList)
            {
                if (setupInfo.uid == uid)
                {
                    return setupInfo;
                }
            }
            return null;
        }

        public override void OnRelease()
        {
            data = null;
            count = 0;
            foreach (var setupInfo in setupList)
            {
                setupInfo.Release();
            }
            setupList.Clear();
            base.OnRelease();
        }
    }
}

using System.Collections;
using MonMoose.Core;

namespace MonMoose.Logic
{
    public enum ESettingKey
    {
        None,
    }

    public class SettingDefineInitializer : Initializer
    {
        protected override IEnumerator OnProcess()
        {
            yield return null;
        }
    }
}
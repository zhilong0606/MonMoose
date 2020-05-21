using System.Collections;
using MonMoose.Core;

namespace MonMoose.Logic
{
    public enum ESettingKey
    {
        None,
        MineSweeper_Difficulty,
        MineSweeper_BestTime,
        MineSweeper_TotalPlayCount,
        MineSweeper_WinCount,
    }

    public class SettingDefineInitializer : Initializer
    {
        protected override IEnumerator OnProcess()
        {
            yield return null;
        }
    }
}
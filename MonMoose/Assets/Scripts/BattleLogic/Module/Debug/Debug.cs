using System.Collections;
using System.Collections.Generic;

namespace MonMoose.BattleLogic
{
    internal static class Debug
    {
        private const int m_logLevel = 0;
        private const int m_errorLevel = 2;

        public static void Log(BattleBase battleInstance, string str)
        {
            battleInstance.Log(m_logLevel, str);
        }

        public static void Log<T1>(BattleBase battleInstance, string str, T1 p1)
        {
            Log(battleInstance, string.Format(str, p1));
        }

        public static void Log<T1, T2>(BattleBase battleInstance, string str, T1 p1, T2 p2)
        {
            Log(battleInstance, string.Format(str, p1, p2));
        }

        public static void Log<T1, T2, T3>(BattleBase battleInstance, string str, T1 p1, T2 p2, T3 p3)
        {
            Log(battleInstance, string.Format(str, p1, p2, p3));
        }

        public static void Log<T1, T2, T3, T4>(BattleBase battleInstance, string str, T1 p1, T2 p2, T3 p3, T4 p4)
        {
            Log(battleInstance, string.Format(str, p1, p2, p3, p4));
        }

        public static void Log<T1, T2, T3, T4, T5>(BattleBase battleInstance, string str, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5)
        {
            Log(battleInstance, string.Format(str, p1, p2, p3, p4, p5));
        }

        public static void LogError(BattleBase battleInstance, string str)
        {
            battleInstance.Log(m_errorLevel, str);
        }

        public static void LogError<T1>(BattleBase battleInstance, string str, T1 p1)
        {
            LogError(battleInstance, string.Format(str, p1));
        }

        public static void LogError<T1, T2>(BattleBase battleInstance, string str, T1 p1, T2 p2)
        {
            LogError(battleInstance, string.Format(str, p1, p2));
        }

        public static void LogError<T1, T2, T3>(BattleBase battleInstance, string str, T1 p1, T2 p2, T3 p3)
        {
            LogError(battleInstance, string.Format(str, p1, p2, p3));
        }

        public static void LogError<T1, T2, T3, T4>(BattleBase battleInstance, string str, T1 p1, T2 p2, T3 p3, T4 p4)
        {
            LogError(battleInstance, string.Format(str, p1, p2, p3, p4));
        }

        public static void LogError<T1, T2, T3, T4, T5>(BattleBase battleInstance, string str, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5)
        {
            LogError(battleInstance, string.Format(str, p1, p2, p3, p4, p5));
        }
    }
}

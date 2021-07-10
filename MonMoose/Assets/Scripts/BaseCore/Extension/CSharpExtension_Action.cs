﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Core
{
    public static class CSharpExtension_Action
    {
        public static void InvokeSafely(this Action action)
        {
            if (action != null)
            {
                action();
            }
        }

        public static void InvokeSafely<T0>(this Action<T0> action, T0 p0)
        {
            if (action != null)
            {
                action(p0);
            }
        }

        public static void InvokeSafely<T0, T1>(this Action<T0, T1> action, T0 p0, T1 p1)
        {
            if (action != null)
            {
                action(p0, p1);
            }
        }

        public static void InvokeSafely<T0, T1, T2>(this Action<T0, T1, T2> action, T0 p0, T1 p1, T2 p2)
        {
            if (action != null)
            {
                action(p0, p1, p2);
            }
        }

        public static void InvokeSafely<T0, T1, T2, T3>(this Action<T0, T1, T2, T3> action, T0 p0, T1 p1, T2 p2, T3 p3)
        {
            if (action != null)
            {
                action(p0, p1, p2, p3);
            }
        }

        public static void InvokeSafely<T0, T1, T2, T3, T4>(this Action<T0, T1, T2, T3, T4> action, T0 p0, T1 p1, T2 p2, T3 p3, T4 p4)
        {
            if (action != null)
            {
                action(p0, p1, p2, p3, p4);
            }
        }
    }
}

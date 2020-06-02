using System;
using UnityEngine;

namespace MonMoose.Logic.Battle
{
    [Serializable]
    public struct GridPosition
    {
        [SerializeField]
        public int x;
        [SerializeField]
        public int y;

        public GridPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        
        public static bool operator ==(GridPosition lhs, GridPosition rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y;
        }

        public static bool operator !=(GridPosition lhs, GridPosition rhs)
        {
            return lhs.x != rhs.x || lhs.y != rhs.y;
        }
    }
}
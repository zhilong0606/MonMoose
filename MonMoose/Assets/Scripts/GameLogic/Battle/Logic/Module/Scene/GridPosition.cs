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

        public static GridPosition operator +(GridPosition lhs, GridPosition rhs)
        {
            lhs.x += rhs.x;
            lhs.y += rhs.y;
            return lhs;
        }

        public static GridPosition operator -(GridPosition lhs, GridPosition rhs)
        {
            lhs.x -= rhs.x;
            lhs.y -= rhs.y;
            return lhs;
        }

        public int DistanceTo(GridPosition pos)
        {
            return Math.Abs(pos.x - x) + Math.Abs(pos.y - y);
        }

        public DcmVec2 ToFix()
        {
            return new DcmVec2(x, y);
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", x, y);
        }
    }
}
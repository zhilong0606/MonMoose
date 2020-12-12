
namespace MonMoose.GameLogic
{
    public static partial class LayerUtility
    {
        public static int GetLayerMask(ELayerMaskType type)
        {
            return m_layerMaskList[(int)type];
        }

        public static bool GetLayerInMask(int mask, ELayerType layerType)
        {
            return (mask & (1 << (int)layerType)) > 0;
        }

        public static int SetLayerInMask(int mask, ELayerType layerType, bool flag)
        {
            if (flag)
            {
                return mask | (1 << (int)layerType);
            }
            else
            {
                return mask & ~(1 << (int)layerType);
            }
        }

        public static bool GetLayerInMask(ELayerMaskType maskType, ELayerType layerType)
        {
            return GetLayerInMask(GetLayerMask(maskType), layerType);
        }

        public static int SetLayerInMask(ELayerMaskType maskType, ELayerType layerType, bool flag)
        {
            return SetLayerInMask(GetLayerMask(maskType), layerType, flag);
        }
    }
}

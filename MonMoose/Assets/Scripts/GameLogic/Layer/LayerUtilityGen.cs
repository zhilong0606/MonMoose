using System.Collections.Generic;

namespace MonMoose.Logic
{
	public enum ELayerMaskType 
	{
		Grid,
		Count
	}
	
	public static partial class LayerUtility 
	{
		private static List<int> m_layerMaskList = new List<int>() 
		{
			256, 	//BattleGrid 
		};
	};
};

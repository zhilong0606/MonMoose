using System.Collections.Generic;
using MonMoose.StaticData;

namespace MonMoose.StaticData
{
	public partial class StaticDataManager
	{
		partial void OnInitLoaders()
		{
			m_loaderMap.Add("Actor", new ProtoDataLoader<ActorStaticInfo, ActorStaticInfoList>(m_ActorList, ActorStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("Attribute", new ProtoDataLoader<AttributeStaticInfo, AttributeStaticInfoList>(m_AttributeList, AttributeStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("BattleScene", new ProtoDataLoader<BattleSceneStaticInfo, BattleSceneStaticInfoList>(m_BattleSceneList, BattleSceneStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("BattleStage", new ProtoDataLoader<BattleStageStaticInfo, BattleStageStaticInfoList>(m_BattleStageList, BattleStageStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("Grid", new ProtoDataLoader<GridStaticInfo, GridStaticInfoList>(m_GridList, GridStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
		}

		//Actor
		private List<ActorStaticInfo> m_ActorList = new List<ActorStaticInfo>();
		public ActorStaticInfo GetActorStaticInfo(int id) { foreach (var info in m_ActorList) if (info.Id == id) return info; return null; }
		public IEnumerator<ActorStaticInfo> GetActorStaticInfoEnumerator() { return m_ActorList.GetEnumerator(); }

		//Attribute
		private List<AttributeStaticInfo> m_AttributeList = new List<AttributeStaticInfo>();
		public AttributeStaticInfo GetAttributeStaticInfo(int id) { foreach (var info in m_AttributeList) if (info.Id == id) return info; return null; }
		public IEnumerator<AttributeStaticInfo> GetAttributeStaticInfoEnumerator() { return m_AttributeList.GetEnumerator(); }

		//BattleScene
		private List<BattleSceneStaticInfo> m_BattleSceneList = new List<BattleSceneStaticInfo>();
		public BattleSceneStaticInfo GetBattleSceneStaticInfo(int id) { foreach (var info in m_BattleSceneList) if (info.Id == id) return info; return null; }
		public IEnumerator<BattleSceneStaticInfo> GetBattleSceneStaticInfoEnumerator() { return m_BattleSceneList.GetEnumerator(); }

		//BattleStage
		private List<BattleStageStaticInfo> m_BattleStageList = new List<BattleStageStaticInfo>();
		public BattleStageStaticInfo GetBattleStageStaticInfo(int id) { foreach (var info in m_BattleStageList) if (info.Id == id) return info; return null; }
		public IEnumerator<BattleStageStaticInfo> GetBattleStageStaticInfoEnumerator() { return m_BattleStageList.GetEnumerator(); }

		//Grid
		private List<GridStaticInfo> m_GridList = new List<GridStaticInfo>();
		public GridStaticInfo GetGridStaticInfo(int id) { foreach (var info in m_GridList) if (info.Id == id) return info; return null; }
		public IEnumerator<GridStaticInfo> GetGridStaticInfoEnumerator() { return m_GridList.GetEnumerator(); }
	}
}

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
			m_loaderMap.Add("Terrain", new ProtoDataLoader<TerrainStaticInfo, TerrainStaticInfoList>(m_TerrainList, TerrainStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("Entity", new ProtoDataLoader<EntityStaticInfo, EntityStaticInfoList>(m_EntityList, EntityStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("PrefabPath", new ProtoDataLoader<PrefabPathStaticInfo, PrefabPathStaticInfoList>(m_PrefabPathList, PrefabPathStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
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

		//Terrain
		private List<TerrainStaticInfo> m_TerrainList = new List<TerrainStaticInfo>();
		public TerrainStaticInfo GetTerrainStaticInfo(int id) { foreach (var info in m_TerrainList) if (info.Id == id) return info; return null; }
		public IEnumerator<TerrainStaticInfo> GetTerrainStaticInfoEnumerator() { return m_TerrainList.GetEnumerator(); }

		//Entity
		private List<EntityStaticInfo> m_EntityList = new List<EntityStaticInfo>();
		public EntityStaticInfo GetEntityStaticInfo(int id) { foreach (var info in m_EntityList) if (info.Id == id) return info; return null; }
		public IEnumerator<EntityStaticInfo> GetEntityStaticInfoEnumerator() { return m_EntityList.GetEnumerator(); }

		//PrefabPath
		private List<PrefabPathStaticInfo> m_PrefabPathList = new List<PrefabPathStaticInfo>();
		public PrefabPathStaticInfo GetPrefabPathStaticInfo(EPrefabPathId id) { foreach (var info in m_PrefabPathList) if (info.Id == (int)id) return info; return null; }
		public IEnumerator<PrefabPathStaticInfo> GetPrefabPathStaticInfoEnumerator() { return m_PrefabPathList.GetEnumerator(); }
	}
}

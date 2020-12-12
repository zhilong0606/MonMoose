using System.Collections.Generic;
using MonMoose.StaticData;

namespace MonMoose.StaticData
{
	public partial class StaticDataManager
	{
		partial void OnInitLoaders()
		{
			m_loaderMap.Add("Actor", new ProtoDataLoader<ActorStaticInfo, ActorStaticInfoList>(m_actorList, ActorStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("Attribute", new ProtoDataLoader<AttributeStaticInfo, AttributeStaticInfoList>(m_attributeList, AttributeStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("Battle", new ProtoDataLoader<BattleStaticInfo, BattleStaticInfoList>(m_battleList, BattleStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("BattleStage", new ProtoDataLoader<BattleStageStaticInfo, BattleStageStaticInfoList>(m_battleStageList, BattleStageStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("Team", new ProtoDataLoader<TeamStaticInfo, TeamStaticInfoList>(m_teamList, TeamStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("Ground", new ProtoDataLoader<GroundStaticInfo, GroundStaticInfoList>(m_groundList, GroundStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("Terrain", new ProtoDataLoader<TerrainStaticInfo, TerrainStaticInfoList>(m_terrainList, TerrainStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("Entity", new ProtoDataLoader<EntityStaticInfo, EntityStaticInfoList>(m_entityList, EntityStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("PrefabPath", new ProtoDataLoader<PrefabPathStaticInfo, PrefabPathStaticInfoList>(m_prefabPathList, PrefabPathStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
		}

		//Actor
		private List<ActorStaticInfo> m_actorList = new List<ActorStaticInfo>();
		public ActorStaticInfo GetActor(int id) { foreach (var info in m_actorList) if (info.Id == id) return info; return null; }
		public IEnumerable<ActorStaticInfo> actorList { get { return m_actorList; } }

		//Attribute
		private List<AttributeStaticInfo> m_attributeList = new List<AttributeStaticInfo>();
		public AttributeStaticInfo GetAttribute(int id) { foreach (var info in m_attributeList) if (info.Id == id) return info; return null; }
		public IEnumerable<AttributeStaticInfo> attributeList { get { return m_attributeList; } }

		//Battle
		private List<BattleStaticInfo> m_battleList = new List<BattleStaticInfo>();
		public BattleStaticInfo GetBattle(int id) { foreach (var info in m_battleList) if (info.Id == id) return info; return null; }
		public IEnumerable<BattleStaticInfo> battleList { get { return m_battleList; } }

		//BattleStage
		private List<BattleStageStaticInfo> m_battleStageList = new List<BattleStageStaticInfo>();
		public BattleStageStaticInfo GetBattleStage(int id) { foreach (var info in m_battleStageList) if (info.Id == id) return info; return null; }
		public IEnumerable<BattleStageStaticInfo> battleStageList { get { return m_battleStageList; } }

		//Team
		private List<TeamStaticInfo> m_teamList = new List<TeamStaticInfo>();
		public TeamStaticInfo GetTeam(int id) { foreach (var info in m_teamList) if (info.Id == id) return info; return null; }
		public IEnumerable<TeamStaticInfo> teamList { get { return m_teamList; } }

		//Ground
		private List<GroundStaticInfo> m_groundList = new List<GroundStaticInfo>();
		public GroundStaticInfo GetGround(int id) { foreach (var info in m_groundList) if (info.Id == id) return info; return null; }
		public IEnumerable<GroundStaticInfo> groundList { get { return m_groundList; } }

		//Terrain
		private List<TerrainStaticInfo> m_terrainList = new List<TerrainStaticInfo>();
		public TerrainStaticInfo GetTerrain(int id) { foreach (var info in m_terrainList) if (info.Id == id) return info; return null; }
		public IEnumerable<TerrainStaticInfo> terrainList { get { return m_terrainList; } }

		//Entity
		private List<EntityStaticInfo> m_entityList = new List<EntityStaticInfo>();
		public EntityStaticInfo GetEntity(int id) { foreach (var info in m_entityList) if (info.Id == id) return info; return null; }
		public IEnumerable<EntityStaticInfo> entityList { get { return m_entityList; } }

		//PrefabPath
		private List<PrefabPathStaticInfo> m_prefabPathList = new List<PrefabPathStaticInfo>();
		public PrefabPathStaticInfo GetPrefabPath(EPrefabPathId id) { foreach (var info in m_prefabPathList) if (info.Id == (int)id) return info; return null; }
		public IEnumerable<PrefabPathStaticInfo> prefabPathList { get { return m_prefabPathList; } }
	}
}

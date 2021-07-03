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
			m_loaderMap.Add("BattleGround", new ProtoDataLoader<BattleGroundStaticInfo, BattleGroundStaticInfoList>(m_battleGroundList, BattleGroundStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("BattleScene", new ProtoDataLoader<BattleSceneStaticInfo, BattleSceneStaticInfoList>(m_battleSceneList, BattleSceneStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("BattleStage", new ProtoDataLoader<BattleStageStaticInfo, BattleStageStaticInfoList>(m_battleStageList, BattleStageStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("BattleTeam", new ProtoDataLoader<BattleTeamStaticInfo, BattleTeamStaticInfoList>(m_battleTeamList, BattleTeamStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("BattleTerrain", new ProtoDataLoader<BattleTerrainStaticInfo, BattleTerrainStaticInfoList>(m_battleTerrainList, BattleTerrainStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("BattleWinCondition", new ProtoDataLoader<BattleWinConditionStaticInfo, BattleWinConditionStaticInfoList>(m_battleWinConditionList, BattleWinConditionStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("CollectableActor", new ProtoDataLoader<CollectableActorStaticInfo, CollectableActorStaticInfoList>(m_collectableActorList, CollectableActorStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
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

		//BattleGround
		private List<BattleGroundStaticInfo> m_battleGroundList = new List<BattleGroundStaticInfo>();
		public BattleGroundStaticInfo GetBattleGround(int id) { foreach (var info in m_battleGroundList) if (info.Id == id) return info; return null; }
		public IEnumerable<BattleGroundStaticInfo> battleGroundList { get { return m_battleGroundList; } }

		//BattleScene
		private List<BattleSceneStaticInfo> m_battleSceneList = new List<BattleSceneStaticInfo>();
		public BattleSceneStaticInfo GetBattleScene(int id) { foreach (var info in m_battleSceneList) if (info.Id == id) return info; return null; }
		public IEnumerable<BattleSceneStaticInfo> battleSceneList { get { return m_battleSceneList; } }

		//BattleStage
		private List<BattleStageStaticInfo> m_battleStageList = new List<BattleStageStaticInfo>();
		public BattleStageStaticInfo GetBattleStage(int id) { foreach (var info in m_battleStageList) if (info.Id == id) return info; return null; }
		public IEnumerable<BattleStageStaticInfo> battleStageList { get { return m_battleStageList; } }

		//BattleTeam
		private List<BattleTeamStaticInfo> m_battleTeamList = new List<BattleTeamStaticInfo>();
		public BattleTeamStaticInfo GetBattleTeam(int id) { foreach (var info in m_battleTeamList) if (info.Id == id) return info; return null; }
		public IEnumerable<BattleTeamStaticInfo> battleTeamList { get { return m_battleTeamList; } }

		//BattleTerrain
		private List<BattleTerrainStaticInfo> m_battleTerrainList = new List<BattleTerrainStaticInfo>();
		public BattleTerrainStaticInfo GetBattleTerrain(int id) { foreach (var info in m_battleTerrainList) if (info.Id == id) return info; return null; }
		public IEnumerable<BattleTerrainStaticInfo> battleTerrainList { get { return m_battleTerrainList; } }

		//BattleWinCondition
		private List<BattleWinConditionStaticInfo> m_battleWinConditionList = new List<BattleWinConditionStaticInfo>();
		public BattleWinConditionStaticInfo GetBattleWinCondition(int id) { foreach (var info in m_battleWinConditionList) if (info.Id == id) return info; return null; }
		public IEnumerable<BattleWinConditionStaticInfo> battleWinConditionList { get { return m_battleWinConditionList; } }

		//CollectableActor
		private List<CollectableActorStaticInfo> m_collectableActorList = new List<CollectableActorStaticInfo>();
		public CollectableActorStaticInfo GetCollectableActor(int id) { foreach (var info in m_collectableActorList) if (info.Id == id) return info; return null; }
		public IEnumerable<CollectableActorStaticInfo> collectableActorList { get { return m_collectableActorList; } }

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

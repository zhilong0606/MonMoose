using System.Collections.Generic;
using MonMoose.StaticData;

namespace MonMoose.Core
{
	public partial class StaticDataManager
	{
		partial void OnInit()
		{
			m_loaderMap.Add("Actor", new ProtoDataLoader<ActorStaticInfo, ActorStaticInfoList>(m_ActorList, ActorStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
			m_loaderMap.Add("Attribute", new ProtoDataLoader<AttributeStaticInfo, AttributeStaticInfoList>(m_AttributeList, AttributeStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
		}

		//Actor
		private List<ActorStaticInfo> m_ActorList = new List<ActorStaticInfo>();
		public ActorStaticInfo GetActorStaticInfo(int id) { foreach (var info in m_ActorList) if (info.Id == id) return info; return null; }
		public IEnumerator<ActorStaticInfo> GetActorStaticInfoEnumerator() { return m_ActorList.GetEnumerator(); }

		//Attribute
		private List<AttributeStaticInfo> m_AttributeList = new List<AttributeStaticInfo>();
		public AttributeStaticInfo GetAttributeStaticInfo(int id) { foreach (var info in m_AttributeList) if (info.Id == id) return info; return null; }
		public IEnumerator<AttributeStaticInfo> GetAttributeStaticInfoEnumerator() { return m_AttributeList.GetEnumerator(); }
	}
}

using System.Collections.Generic;
using MonMoose.StaticData;

public partial class StaticDataManager
{
	partial void OnInit()
	{
		m_loaderMap.Add("Actor", new ProtoDataLoader<ActorStaticInfo, ActorStaticInfoList>(m_ActorList, ActorStaticInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
	}

	//Actor
	private List<ActorStaticInfo> m_ActorList = new List<ActorStaticInfo>();
	public ActorStaticInfo GetActorStaticInfo(int id) { foreach (var info in m_ActorList) if (info.Id == id) return info; return null; }
	public IEnumerator<ActorStaticInfo> GetActorStaticInfoEnumerator() { return m_ActorList.GetEnumerator(); }
}

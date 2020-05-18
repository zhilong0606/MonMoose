using System.Collections.Generic;
using MonMoose.ConfigData;

public partial class StaticDataManager
{
	partial void OnInit()
	{
		m_loaderMap.Add("TestDataInfo", new ProtoDataLoader<ConfigDataTestDataInfo, ConfigDataTestDataInfoList>(m_TestDataInfoList, ConfigDataTestDataInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
		m_loaderMap.Add("WeaponInfo", new ProtoDataLoader<ConfigDataWeaponInfo, ConfigDataWeaponInfoList>(m_WeaponInfoList, ConfigDataWeaponInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
		m_loaderMap.Add("EquipInfo", new ProtoDataLoader<ConfigDataEquipInfo, ConfigDataEquipInfoList>(m_EquipInfoList, ConfigDataEquipInfoList.Parser.ParseFrom, (fromList, toList) => { toList.Clear(); toList.AddRange(fromList.List); }));
	}

	//TestDataInfo
	private List<ConfigDataTestDataInfo> m_TestDataInfoList = new List<ConfigDataTestDataInfo>();
	public ConfigDataTestDataInfo GetTestDataInfo(int id) { foreach (var info in m_TestDataInfoList) if (info.Id == id) return info; return null; }
	public IEnumerator<ConfigDataTestDataInfo> GetTestDataInfoEnumerator() { return m_TestDataInfoList.GetEnumerator(); }

	//WeaponInfo
	private List<ConfigDataWeaponInfo> m_WeaponInfoList = new List<ConfigDataWeaponInfo>();
	public ConfigDataWeaponInfo GetWeaponInfo(int id) { foreach (var info in m_WeaponInfoList) if (info.Id == id) return info; return null; }
	public IEnumerator<ConfigDataWeaponInfo> GetWeaponInfoEnumerator() { return m_WeaponInfoList.GetEnumerator(); }

	//EquipInfo
	private List<ConfigDataEquipInfo> m_EquipInfoList = new List<ConfigDataEquipInfo>();
	public ConfigDataEquipInfo GetEquipInfo(int id) { foreach (var info in m_EquipInfoList) if (info.Id == id) return info; return null; }
	public IEnumerator<ConfigDataEquipInfo> GetEquipInfoEnumerator() { return m_EquipInfoList.GetEnumerator(); }
}

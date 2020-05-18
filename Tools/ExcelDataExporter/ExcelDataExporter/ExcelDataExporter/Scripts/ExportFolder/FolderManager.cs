using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FolderManager : Singleton<FolderManager>
{
    private Dictionary<string, FolderGroupInfo> m_folderGroupInfoMap = new Dictionary<string, FolderGroupInfo>();

    private FolderGroupInfo GetFolderGroupInfo(string path)
    {
        FolderGroupInfo groupInfo;
        if (!m_folderGroupInfoMap.TryGetValue(path, out groupInfo))
        {
            groupInfo = new FolderGroupInfo(path);
            m_folderGroupInfoMap.Add(path, groupInfo);
        }
        return groupInfo;
    }

    public string GetSubDirPath(string path, EFolderType type)
    {
        FolderGroupInfo groupInfo = GetFolderGroupInfo(path);
        if (groupInfo != null)
        {
            return groupInfo.GetSubDirInfo(type).FullName;
        }
        return null;
    }

    public void ClearSubDir(string path, EFolderType type)
    {
        FolderGroupInfo groupInfo = GetFolderGroupInfo(path);
        if (groupInfo != null)
        {
            groupInfo.ClearSubDir(type);
        }
    }
}

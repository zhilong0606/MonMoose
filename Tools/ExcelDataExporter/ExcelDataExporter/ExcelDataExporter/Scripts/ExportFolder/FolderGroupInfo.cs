using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FolderGroupInfo
{
    private const string m_splitMark = "\\";
    private DirectoryInfo m_dirInfo;
    private Dictionary<int, DirectoryInfo> m_dirMap = new Dictionary<int, DirectoryInfo>();

    public DirectoryInfo dirInfo
    {
        get { return m_dirInfo; }
    }

    public FolderGroupInfo(string path)
    {
        m_dirInfo = GetOrCreateDirectoryInfo(path);
        foreach (object obj in Enum.GetValues(typeof(EFolderType)))
        {
            string subDirPath = path + m_splitMark + obj;
            DirectoryInfo subInfo = GetOrCreateDirectoryInfo(subDirPath);
            m_dirMap.Add((int)obj, subInfo);
        }
    }

    public DirectoryInfo GetSubDirInfo(EFolderType folderType)
    {
        return m_dirMap[(int)folderType];
    }

    public void ClearSubDir(EFolderType folderType)
    {
        foreach (FileInfo fileInfo in m_dirMap[(int)folderType].GetFiles())
        {
            fileInfo.Delete();
        }
    }

    private DirectoryInfo GetOrCreateDirectoryInfo(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        return new DirectoryInfo(path);
    }
}

using System;
using System.IO;
using System.Xml.Serialization;


[XmlRoot("Config")]
[Serializable]
public class Config
{
    [XmlElement]
    public string excelFolderPath = string.Empty;
    [XmlElement]
    public User client = new User();
    [XmlElement]
    public User server = new User();
    [XmlElement]
    public bool isDebugMode = false;
    [XmlElement]
    public bool needLogOut = true;
    [XmlElement]
    public bool needGenerateStructure = true;
    [XmlElement]
    public bool needGenerateData = true;
    [XmlElement]
    public bool needGenerateLoader = true;

    [Serializable]
    public class User
    {
        [XmlElement]
        public bool needExport = true;
        [XmlElement]
        public string usingNameSpace = string.Empty;
        [XmlElement]
        public string nameSpace = string.Empty;
        [XmlElement]
        public string prefix = string.Empty;
        [XmlElement]
        public string postfix = string.Empty;
        [XmlElement]
        public string structureExportPath = string.Empty;
        [XmlElement]
        public string dataExportPath = string.Empty;
        [XmlElement]
        public EExportModeType exportMode = EExportModeType.Flat;
    }

    private static string m_configPath = "Config.xml";

    private static Config m_instance = null;

    public static Config instance
    {
        get { return m_instance; }
    }
    
    public static void LoadConfig()
    {
        using (FileStream stream = File.Open(m_configPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            XmlSerializer serialize = new XmlSerializer(typeof(Config));
            try
            {
                m_instance = serialize.Deserialize(stream) as Config;
            }
            catch
            {
            }
        }
    }

    public static void SaveConfig()
    {
        using (FileStream stream = File.Open(m_configPath, FileMode.Create, FileAccess.ReadWrite))
        {
            XmlSerializer serialize = new XmlSerializer(typeof(Config));
            serialize.Serialize(stream, m_instance);
        }
    }
}

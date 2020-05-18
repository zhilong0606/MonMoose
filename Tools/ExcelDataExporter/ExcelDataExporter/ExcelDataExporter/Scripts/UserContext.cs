using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class UserContext
{
    public string name;
    public string namespaceStr;
    public string prefixStr;
    public string structureExportPath;
    public string dataExportPath;
    public bool needExport;
    public ExportMode exportMode;
    public Assembly assembly;
    public List<string> tagNameList = new List<string>();
}

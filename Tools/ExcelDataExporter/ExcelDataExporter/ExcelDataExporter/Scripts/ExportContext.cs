using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ExportContext
{
    public bool needGenerateStructure;
    public bool needGenerateData;
    public bool needGenerateLoader;
    public List<UserContext> userContextList = new List<UserContext>();
}

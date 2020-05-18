using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GenerateStructureProcessStep : UserContextProcessStep
{
    protected override EExportProcessType processType
    {
        get { return EExportProcessType.GenerateClass; }
    }

    public GenerateStructureProcessStep(UserContext context) : base(context)
    {
    }

    protected override void OnExecute()
    {
        SendMsg(0, "正在生成结构代码");
        Type exportType = m_context.exportMode.structureExporterType;
        StructureExporter exporter = Activator.CreateInstance(exportType) as StructureExporter;
        exporter.actionOnProcessMsgSend = OnProcessMsgSend;
        exporter.Export(m_context);
        SendMsg(1, "生成结构代码完毕");
    }
}

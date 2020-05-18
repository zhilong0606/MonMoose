using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GenerateDataProcessStep : UserContextProcessStep
{
    protected override EExportProcessType processType
    {
        get { return EExportProcessType.GenerateData; }
    }

    public GenerateDataProcessStep(UserContext context) : base(context)
    {
    }

    protected override void OnExecute()
    {
        SendMsg(0, "正在导出数据");
        Type exportType = m_context.exportMode.dataExporterType;
        DataExporter exporter = Activator.CreateInstance(exportType) as DataExporter;
        exporter.actionOnProcessMsgSend = OnProcessMsgSend;
        exporter.Export(m_context);
        SendMsg(1, "导出数据完毕");
    }
}

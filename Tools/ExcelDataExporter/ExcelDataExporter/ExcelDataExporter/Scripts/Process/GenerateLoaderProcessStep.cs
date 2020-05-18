using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GenerateLoaderProcessStep : UserContextProcessStep
{
    protected override EExportProcessType processType
    {
        get { return EExportProcessType.GenerateLoader; }
    }

    public GenerateLoaderProcessStep(UserContext context) : base(context)
    {
    }

    protected override void OnExecute()
    {
        SendMsg(0, "正在生成读取代码");
        Type exportType = m_context.exportMode.loaderExporterType;
        LoaderExporter exporter = Activator.CreateInstance(exportType) as LoaderExporter;
        exporter.actionOnProcessMsgSend = OnProcessMsgSend;
        exporter.Export(m_context);
        SendMsg(1, "生成读取代码完毕");
    }
}

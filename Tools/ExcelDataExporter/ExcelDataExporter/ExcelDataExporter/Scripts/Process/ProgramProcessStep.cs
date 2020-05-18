using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Data;
using Structure;

public class ProgramProcessStep : ExportContextProcessStep
{
    private List<ProcessStep> m_processList = new List<ProcessStep>();

    protected override EExportProcessType processType
    {
        get { return EExportProcessType.Max; }
    }

    public ProgramProcessStep(ExportContext context) : base(context)
    {
    }

    protected override void OnExecute()
    {
        m_processList.Clear();
        AddProcess(new LoadExcelProcessStep(m_context));
        for (int i = 0; i < m_context.userContextList.Count; ++i)
        {
            UserContext userContext = m_context.userContextList[i];
            if (!userContext.needExport)
            {
                continue;
            }
            //if (Config.instance.needGenerateStructure)
            {
                AddProcess(new GenerateStructureProcessStep(userContext));
            }
            //if (Config.instance.needGenerateData)
            {
                AddProcess(new GenerateDataProcessStep(userContext));
            }
            //if (Config.instance.needGenerateStructure && Config.instance.needGenerateData && Config.instance.needGenerateLoader)
            {
                AddProcess(new GenerateLoaderProcessStep(userContext));
            }
            StructureManager.Instance.Clear();
            DataObjectManager.Instance.Clear();
        }

        for (int i = 0; i < m_processList.Count; ++i)
        {
            m_processList[i].Execute();
        }
        SendMsg(1, "导出完毕");
    }

    private void AddProcess(ProcessStep processStep)
    {
        processStep.actionOnProcessMsgSend = OnProcessMsgSend;
        m_processList.Add(processStep);
    }

    private void OnProcessMsgSend(double processValue, string content)
    {
        SendMsg(processValue, content);
    }
}

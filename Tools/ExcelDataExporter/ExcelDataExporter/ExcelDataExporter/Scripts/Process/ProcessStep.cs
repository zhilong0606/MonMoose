using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class ProcessStep
{
    public Action<double, string> actionOnProcessMsgSend;

    protected abstract EExportProcessType processType { get; }

    public void Execute()
    {
        OnExecute();
    }

    protected abstract void OnExecute();

    protected virtual double GetProcessValue(double value)
    {
        return ((double)processType + value) / (double)EExportProcessType.Max;
    }

    protected void SendMsg(double processValue, string content)
    {
        if (actionOnProcessMsgSend != null)
        {
            actionOnProcessMsgSend(GetProcessValue(processValue), content);
        }
    }

    protected void OnProcessMsgSend(double processValue, string content)
    {
        SendMsg(processValue, content);
    }
}

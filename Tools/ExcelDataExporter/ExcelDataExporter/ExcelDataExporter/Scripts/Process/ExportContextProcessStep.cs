using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class ExportContextProcessStep : ProcessStep
{
    protected ExportContext m_context;

    public ExportContextProcessStep(ExportContext context)
    {
        m_context = context;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class UserContextProcessStep : ProcessStep
{
    public UserContext m_context;

    public UserContextProcessStep(UserContext context)
    {
        m_context = context;
    }
}

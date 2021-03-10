using System;
using System.Collections.Generic;
using System.Text;

namespace DotXxlJob.Core.TaskExecutors
{
    public class ShellGlueExecutor : PowerShellGlueExecutor
    {
        public ShellGlueExecutor(IJobLogger logger): base(logger)
        {
            GlueType = "GLUE_SHELL";
        }
    }
}

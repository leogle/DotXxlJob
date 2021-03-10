using System;
using System.Collections.Generic;
using System.Text;

namespace DotXxlJob.Core.TaskExecutors
{
    public class PythonGlueExecutor : PowerShellGlueExecutor
    {
        public PythonGlueExecutor(IJobLogger logger): base(logger)
        {
            GlueType = "GLUE_PYTHON";
        }
    }
}

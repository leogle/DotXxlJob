using System;
using System.Collections.Generic;
using System.Text;

namespace DotXxlJob.Core.TaskExecutors
{
    public class NodeGlueExecutor : PowerShellGlueExecutor
    {
        public NodeGlueExecutor(IJobLogger logger): base(logger)
        {
            GlueType = "GLUE_NODEJS";
        }
    }
}

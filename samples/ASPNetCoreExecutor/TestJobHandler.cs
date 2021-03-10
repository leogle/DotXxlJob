using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotXxlJob.Core;
using DotXxlJob.Core.Model;

namespace ASPNetCoreExecutor
{
    [JobHandler("testJobHandler")]
    public class TestJobHandler : AbstractJobHandler
    {
        public override Task<ReturnT> Execute(JobExecuteContext context)
        {
            context.JobLogger.Log("receive test job handler,parameter:{0}", context.JobParameter);

            return Task.FromResult(ReturnT.SUCCESS);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotXxlJob.Core;
using DotXxlJob.Core.Model;

namespace ASPNetCoreExecutor
{
    public class DotNetGlueJobHandler : ITaskExecutor
    {
        private readonly IJobLogger _jobLogger;

        public DotNetGlueJobHandler(IJobLogger logger)
        {
            _jobLogger = logger;
        }
        public string GlueType => "DotNet";

        public Task<ReturnT> Execute(TriggerParam triggerParam)
        {
            var handler = GlueFactory.Instance.LoadNewInstance(triggerParam.GlueSource);

            if (handler == null)
            {
                return Task.FromResult(ReturnT.Failed($"job handler Glue not found."));
            }
            var context = new JobExecuteContext(this._jobLogger, triggerParam.ExecutorParams, triggerParam.BroadcastIndex, triggerParam.BroadcastTotal);
            return handler.Execute(context);
        }
    }
}

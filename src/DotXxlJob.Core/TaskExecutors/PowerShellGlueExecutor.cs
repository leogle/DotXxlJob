using System.Diagnostics;
using System.Threading.Tasks;
using DotXxlJob.Core.DefaultHandlers;
using DotXxlJob.Core.Model;

namespace DotXxlJob.Core.TaskExecutors
{
    public class PowerShellGlueExecutor : ITaskExecutor
    {
        private readonly IJobLogger _jobLogger;

        public PowerShellGlueExecutor(IJobLogger logger)
        {
            _jobLogger = logger;
        }
        public string GlueType { get; protected set; } = "GLUE_POWERSHELL";

        public Task<ReturnT> Execute(TriggerParam triggerParam)
        {
            ScriptHandler script = new ScriptHandler();
            var context = new JobExecuteContext(_jobLogger, triggerParam.ExecutorParams, triggerParam.BroadcastIndex, triggerParam.BroadcastTotal);
            context.JobId = triggerParam.JobId;
            context.GlueSource = triggerParam.GlueSource;
            context.GlueType = triggerParam.GlueType;
            return script.Execute(context);
        }
    }
}

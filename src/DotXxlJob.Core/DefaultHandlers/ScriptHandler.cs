using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DotXxlJob.Core.Model;

namespace DotXxlJob.Core.DefaultHandlers
{
    [JobHandler("scriptJobHandler")]
    public class ScriptHandler : AbstractJobHandler
    {
        public override Task<ReturnT> Execute(JobExecuteContext context)
        {
            var fileName = GetFileName(context);
            using(FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                using(StreamWriter sr = new StreamWriter(fs,Encoding.UTF8))
                {
                    sr.Write(context.GlueSource);
                    sr.Flush();
                }
            }
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = GetCmd(context.GlueType);
            processStartInfo.Arguments = "./"+fileName+" "+context.ShardIndex+" "+context.ShardTotal;
            processStartInfo.RedirectStandardOutput = true;
            if (context.GlueType != "GLUE_POWERSHELL")
            {
                processStartInfo.StandardOutputEncoding = Encoding.UTF8;
            }
            var proc = Process.Start(processStartInfo);
            var log = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            context.JobLogger.Log(log);
            if (proc.ExitCode >= 0)
            {
                return Task.FromResult(ReturnT.SUCCESS);
            }
            return Task.FromResult(ReturnT.FAIL);
        }

        private string GetFileName(JobExecuteContext context)
        {
            var name = context.JobId + "";
            switch (context.GlueType)
            {
                case "GLUE_POWERSHELL":
                    name += ".ps1";
                    break;
                case "GLUE_SHELL":
                    name += ".sh";
                    break;
                case "GLUE_PYTHON":
                    name += ".py";
                    break;
                case "GLUE_NODEJS":
                    name += ".js";
                    break;
                default:
                    return "sh";
            }
            return name;
        }

        public string GetCmd(string glueType)
        {
            switch (glueType)
            {
                case "GLUE_POWERSHELL":
                    return "powershell";
                case "GLUE_SHELL":
                    return "bash";
                case "GLUE_PYTHON":
                    return "python";
                case "GLUE_NODEJS":
                    return "node";
                default:
                    return "sh";
            }
        }
    }
}

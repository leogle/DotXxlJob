namespace DotXxlJob.Core.Model
{
    public class JobExecuteContext
    {
        public JobExecuteContext(IJobLogger jobLogger,string jobParameter,int shardIndex,int shardTotal)
        {
            this.JobLogger = jobLogger;
            this.JobParameter = jobParameter;
            this.ShardIndex = shardIndex;
            this.ShardTotal = shardTotal;
        }
        public JobExecuteContext(IJobLogger jobLogger, string jobParameter, int shardIndex, int shardTotal,string glueType,string glueSrc)
        {
            this.JobLogger = jobLogger;
            this.JobParameter = jobParameter;
            this.ShardIndex = shardIndex;
            this.ShardTotal = shardTotal;
            this.GlueSource = glueSrc;
            this.GlueType = glueType;
        }
        public string JobParameter { get; }
        public IJobLogger JobLogger { get;  }

        public int ShardIndex { get; }
        public int ShardTotal { get; }

        public string GlueSource { get; set; }
        public string GlueType { get; set; }
        public int JobId { get; set; }
    }
}
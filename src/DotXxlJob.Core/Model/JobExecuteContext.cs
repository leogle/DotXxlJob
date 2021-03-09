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
        public string JobParameter { get; }
        public IJobLogger JobLogger { get;  }

        public int ShardIndex { get; }
        public int ShardTotal { get; }
    }
}
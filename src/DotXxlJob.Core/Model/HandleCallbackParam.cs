using System.Runtime.Serialization;

namespace DotXxlJob.Core.Model
{
    [DataContract(Name = Constants.HandleCallbackParamJavaFullName)]
    public class HandleCallbackParam
    {
        public HandleCallbackParam()
        {
            
        }
        public HandleCallbackParam(TriggerParam triggerParam, ReturnT result)
        {
            this.LogId = triggerParam.LogId;
            this.LogDateTime = triggerParam.LogDateTime;
            this.HandleCode = result.Code;
            this.HandleMsg = result.Msg;
        }
        
       
        public int CallbackRetryTimes { get; set; }
        
        [DataMember(Name = "logId",Order = 1)]
        public long LogId { get; set; }
        [DataMember(Name = "logDateTim",Order = 2)]
        public long LogDateTime { get; set; }
        //[DataMember(Name = "executeResult",Order = 3)]
        //public ReturnT ExecuteResult { get; set; }
        [DataMember(Name = "handleCode", Order = 3)]
        public int HandleCode { get; set; }

        [DataMember(Name = "handleMsg", Order = 4)]
        public string HandleMsg { get; set; }
    }
}
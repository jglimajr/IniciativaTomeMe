using System.ComponentModel;
using System.Text.Json.Serialization;
using InteliSystem.Utils.Enumerators;

namespace InteliSystem.Utils.GlobalClasses
{
    public class ClassReturn
    {
        public ClassReturn(ReturnValues status, object? value, long records = 0, int totalpages = 0, string message = "")
        {
            this.Status = status;
            this.Value = value;
            this.Records = records;
            this.TotalPages = totalpages;
            this.Message = message;
        }
        public ReturnValues Status { get; private set; }
        public dynamic? Value { get; private set; }
        public string Message { get; private set; }
        [JsonIgnore()]
        [Bindable(true)]
        public long Records { get; private set; }
        [JsonIgnore()]
        [Bindable(true)]
        public int TotalPages { get; private set; }
    }
}
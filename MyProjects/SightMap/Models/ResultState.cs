using SightMap.BLL.Infrastructure;

namespace SightMap.Models
{
    public class ResultState<T>
    {
        public ResultState() { }
        public ResultState(T resultObject) : this(resultObject, null)
        {
        }
        public ResultState(T resultObject, string message)
        {
            IsSuccess = resultObject != null;
            Value = resultObject;
            Message = resultObject is null ? Constants.ErrorSmthWrong : message;
        }
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string Message { get; set; }

    }
}

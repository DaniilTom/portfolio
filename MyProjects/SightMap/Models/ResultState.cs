using SightMap.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SightMap.Models
{
    public class ResultState<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string Message { get; set; }

        public ResultState() { }
        public ResultState(T resultObject)
        { 
            if (!(resultObject is null))
            {
                IsSuccess = true;
                Value = resultObject;
            }
            else
            {
                IsSuccess = false;
                Value = null;
                Message = Constants.ErrorSmthWrong;
            }
        }
        public ResultState(T resultObject, string Message) : this(resultObject)
        {
            this.Message = Message;
        }
    }
}

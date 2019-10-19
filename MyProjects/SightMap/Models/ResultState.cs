using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SightMap.Models
{
    public class ResultState<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string Message { get; set; }

        public static ResultState<T> CreateResulState<T>(T resultObject) where T : class
        {
            ResultState<T> resultState = new ResultState<T>();
            if (!(resultObject is null))
            {
                resultState.IsSuccess = true;
                resultState.Value = resultObject;
            }
            else
            {
                resultState.IsSuccess = false;
                resultState.Value = null;
                resultState.Message = "Что-то пошло не так.";
            }
            return resultState;
        }
    }
}

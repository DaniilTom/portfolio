﻿using SightMap.BLL.Infrastructure;

namespace SightMap.Models
{
    public class ResultState<T> where T : class
    {
        public ResultState() { }
        public ResultState(T resultObject) : this(resultObject, null)
        {
        }
        public ResultState(T resultObject, string message)
        {
            IsSuccess = resultObject != null;
            Value = resultObject;
            Message = string.IsNullOrWhiteSpace(message) ? Constants.ErrorSmthWrong : message;
        }
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string Message { get; set; }

    }
}

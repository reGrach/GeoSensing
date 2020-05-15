using Newtonsoft.Json;
using System;

namespace GeoService.BLL
{
    public class ApiException : Exception
    {
        public string LogMsg { get; set; }
        public int StatusCode { get; set; }
        public object Value { get; set; }
        public ApiException(object value, string methodName, int statusCode) : base(JsonConvert.SerializeObject(value))
        {
            LogMsg = $"Ошибка в методе {methodName}. {Message}";
            StatusCode = statusCode;
            Value = value;
        }
    }
}

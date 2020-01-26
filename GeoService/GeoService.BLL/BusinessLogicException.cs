using System;
namespace GeoService.BLL
{
    public class BusinessLogicException : Exception
    {
        public BusinessLogicException() { }

        public BusinessLogicException(string message) : base(message) { }

        public BusinessLogicException(string message, Exception inner) : base(message, inner) { }
    }
}

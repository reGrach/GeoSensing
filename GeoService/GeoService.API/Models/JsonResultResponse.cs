namespace GeoService.API.Models
{
    public class JsonResultResponse
    {
        public bool Success { get; set; }
        public object Result { get; set; }
        public JsonResultResponse() { }
        public JsonResultResponse(bool success, object result)
        {
            Success = success;
            Result = result;
        }

        public static JsonResultResponse Ok(object result)
        {
            return new JsonResultResponse(true, result);
        }

        public static JsonResultResponse Bad(string msg)
        {
            return new JsonResultResponse(false, msg);
        }
    }
}

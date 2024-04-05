using static System.Runtime.InteropServices.JavaScript.JSType;

namespace N.Service.Dto
{
    public class DataResponse<T> 
    {
        public string? Message { get; set; }
        public T? Data { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string?>? Errors { get; set; }

        public static DataResponse<T> False(string message, IEnumerable<string>? errors = null)
        {
            return new DataResponse<T>()
            {
                Success = false,
                Message = message,
                Errors = errors,
            };
        }
        public static DataResponse<T> True(T data, string message = "Success")
        {
            return new DataResponse<T>()
            {
                Success = true,
                Message = message,
                Data = data,
            };
        }
    }

    public class DataResponse : DataResponse<object>
    {
        public static new DataResponse False(string message, IEnumerable<string>? errors = null)
        {
            return new DataResponse()
            {
                Success = false,
                Message = message,
                Errors = errors,
            };
        }
        public static new DataResponse True(object? data = null, string message = "Success")
        {
            return new DataResponse()
            {
                Success = true,
                Message = message,
                Data = data,
            };
        }

    }


}

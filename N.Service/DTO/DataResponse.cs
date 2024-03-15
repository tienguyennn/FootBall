namespace N.Service.DTO
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
    }


}

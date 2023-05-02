namespace tadoba_api.Models
{
    public class Response<T>
    {
        public T Data { set; get; }
        public bool IsSuccess { set; get; }
        public string Message { set; get; }
        public Response()
        {
            IsSuccess = false;
            Message = string.Empty;
            Data = default(T);
        }
        public static async Task<Response<T>> GenerateResponse(bool isSuccess, T data, string message = "")
        {
            return await Task.Run(() =>
            {
                var response = new Response<T>();
                response.Data = data;
                response.IsSuccess = isSuccess;
                response.Message = message;
                return response;
            });

        }
    }
}

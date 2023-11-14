namespace Ecommerce.Dto.Response
{
    public class ResponseDto<T>
    {
        public T? Data { get; set; }

        public int Status { get; set; } = 200;
        public bool Success { get; set; } = true;
        public string Message { get; set; } = String.Empty;

        public ResponseDto()
        {
            Status = 200;
            Success = true;
            Message = String.Empty;
        }

        public ResponseDto<T> Response404(string message)
        {
            Status = 404;
            Success = false;
            Message = message;
            return this;
        }

        public ResponseDto<T> NotFound(string message)
        {
            Status = 404;
            Success = false;
            Message = message;
            return this;
        }


        public ResponseDto<T> ResponseSuccess(string message)
        {
            Status = 200;
            Success = true;
            Message = message;
            return this;
        }

        public ResponseDto<T> ResponseData(T data)
        {
            Status = 200;
            Data = data;
            Success = true;
            Message = "Successfully";
            return this;
        }



        public ResponseDto<T> ResponseError(string message)
        {
            Status = 500;
            Success = false;
            Message = message;
            return this;
        }
    }

}


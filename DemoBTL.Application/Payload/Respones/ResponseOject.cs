namespace DemoBTL.Application.Payload.Respones
{
    public class ResponseOject<T>
    {
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public ResponseOject() { }
        public ResponseOject(int status, string message, T? data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}

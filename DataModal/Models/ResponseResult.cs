namespace DataModal.Models
{
    public class ResponseResult<T>
    {
        public T Data { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}

using System.Net;

namespace App.Models
{
    public class ReturnValueResponse<T> where T : class
    {
        public string SuccessMessage { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public HttpStatusCode StatusCode { get; set; }
        public T Data { get; set; }
    }
}

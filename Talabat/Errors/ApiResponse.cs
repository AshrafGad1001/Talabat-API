
namespace Talabat.APIs.Errors
{
    public class ApiResponse
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public ApiResponse(int statusCode, string message = null)
        {
            this.statusCode = statusCode;
            this.message = message ?? GetDefaultValueForStatusCode(statusCode);
        }

        private string GetDefaultValueForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "bad Request",
                401 => "Authorized,you are not",
                404 => "Resourse not Found",
                500 => "Server Error ",
                _ => null
            };
        }
    }
}

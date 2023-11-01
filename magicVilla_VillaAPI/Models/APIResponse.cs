using System.Diagnostics.Eventing.Reader;
using System.Net;

namespace magicVilla_VillaAPI.Models
{
    public class APIResponse
    {
        public APIResponse() 
        {
            ErrorMessage = new List<string>();
        
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<String> ErrorMessage { get; set; }
        public object Result { get; set; }  

    }
}

using MagicVilla_Utility;
using magicVilla_Web.Models;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;


namespace MagicVilla_Web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse reponseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }
        public BaseService(IHttpClientFactory httpClient)
        {
            this.reponseModel = new();
            this.httpClient = httpClient;
        }
        // Define an asynchronous method that sends an API request and returns a response of type T
        public async Task<T> SendAsync<T>(APIRequest apirequest)
        {
            try
            {
                // Create an HTTP client with a specific name ("MagicAPI")
                var client = httpClient.CreateClient("MagicAPI");

                // Initialize an HttpRequestMessage to set up the request
                HttpRequestMessage message = new HttpRequestMessage();

                // Add headers to the request
                message.Headers.Add("Accept", "application/json");
                //       message.Headers.Add("Content-Type", "application/json");

                // Set the URL for the request
                message.RequestUri = new Uri(apirequest.Url);

                // If there is data to send in the request, serialize it to JSON and add it to the message content
                if (apirequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apirequest.Data),
                        Encoding.UTF8, "application/json");
                }

                // Determine the HTTP method (GET, POST, PUT, DELETE) based on the ApiType
                switch (apirequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                // Send the HTTP request and get the response
                HttpResponseMessage apiResponse = null;
                if (!string.IsNullOrEmpty(apirequest.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apirequest.Token);


                }


                apiResponse = await client.SendAsync(message);

                // Read the response content as a string
                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                try
                {
                    APIResponse ApiResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);
                    if (ApiResponse != null && (apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest
                     || apiResponse.StatusCode == System.Net.HttpStatusCode.NotFound))
                    {
                        ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        ApiResponse.IsSuccess = false;
                        var res = JsonConvert.SerializeObject(ApiResponse);
                        var returnObj = JsonConvert.DeserializeObject<T>(res);
                        return returnObj;
                    }

                }

                catch (Exception e)
                {
                    var exceptionResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return exceptionResponse;
                }
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;


            }

            catch (Exception e)
            {
                // Handle exceptions by creating an APIResponse object with the error message
                var dto = new APIResponse
                {
                    ErrorMessage = new List<string> { Convert.ToString(e.Message) },
                    IsSuccess = false
                };

                // Serialize and deserialize the error response to type T
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);

                // Return the error response
                return APIResponse;
            }
            }
        }
    }


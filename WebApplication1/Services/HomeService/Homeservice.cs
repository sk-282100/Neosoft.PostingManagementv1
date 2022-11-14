using Nancy.Json;
using Newtonsoft.Json.Linq;
using Phoenix.VLE.Web;
using PostingManagement.UI.Helpers;
using PostingManagement.UI.Services.HomeService.Contracts;
using System.Net.Http.Headers;

namespace PostingManagement.UI.Services.HomeService
{
    public class Homeservice : IHomeService
    {
        readonly HttpClientHandler _clientHandler = new HttpClientHandler();

        public Homeservice(HttpClientHandler clientHandler)
        {

            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

        }
        //#region private variables
        //private static HttpClient _client = new HttpClient(new HttpClientHandler { AllowAutoRedirect = false , ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } });
        //readonly string _baseUrl = "https://qaapi-elite.phoenixclassroom.com/classroomv3_api/";
        //readonly string _path = "api/v1/Home";
        //#endregion

        //public Homeservice()
        //{
        //    _client.BaseAddress = new Uri(_baseUrl);
        //    _client.DefaultRequestHeaders.Accept.Clear();
        //    _client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));
        //}

         

        public async Task<string>  DemoService()
        {
            //var uri = "https://localhost:5000/api/v1/Home/DemoService"; //API.Home.GetDemoService(_path);
            //HttpResponseMessage response = _client.GetAsyncExt(uri).Result;            
            //string result = "";
            //if (response.IsSuccessStatusCode)
            //{
            //    var jsonDataProviders = response.Content.ReadAsStringAsync().Result;
            //    result = Convert.ToString(jsonDataProviders);
            //}
            //return result;

            string result ="";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/Home/DemoService"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = apiResponse;
                    ////var jsonResponse = JObject.Parse(apiResponse);
                    ////Console.WriteLine(jsonArrayResponse["data"]);
                    ////var apiresopnse = jsonResponse["data"].ToString();
                    
                    //JavaScriptSerializer js = new JavaScriptSerializer();
                    //result = js.Deserialize<string>(apiResponse);

                    
                }
            }

            return result;
        }
    }
    
}

using System.Net.Http.Headers;
using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace Phoenix.VLE.Web
{
    public static class HttpClientExtensionHelper
    {
        public static Task<HttpResponseMessage> GetAsyncExt(this HttpClient client, string requestUri,string token="")
        {
           // token = string.IsNullOrEmpty(token) ? SessionHelper.CurrentSession.Token : token;
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
            //httpRequestMessage.Headers.Authorization =new AuthenticationHeaderValue("Bearer", token);
            //httpRequestMessage.Headers.Add("USR_LANG", $"{Phoenix.Common.Localization.LocalizationHelper.CurrentSystemLanguageId}");
            var response = client.SendAsync(httpRequestMessage);
            if(!response.Result.IsSuccessStatusCode && response.Result.StatusCode == HttpStatusCode.Unauthorized)
            {
               // var newToken = RenewToken(client);
                //if (string.IsNullOrWhiteSpace(newToken) == false)
                //{
                //    var newHttpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
                //    newHttpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
                //    return client.SendAsync(newHttpRequestMessage);
                //}
            }
            return response;
        }
        //public static Task<HttpResponseMessage> PostAsJsonAsyncExt<T>(this HttpClient client, string requestUri, T value, string token = "", bool isPhoenixAPI = false)
        //{
        //    //token = isPhoenixAPI ? SessionHelper.CurrentSession.PhoenixAccessToken : SessionHelper.CurrentSession.Token;
        //    //var serializer = new JavaScriptSerializer();
        //    var serializedResult = JsonConvert.SerializeObject(value);
        //    var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = new StringContent(serializedResult, Encoding.UTF8, "application/json") };
        //    httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //   // httpRequestMessage.Headers.Add("X-Client-Agent", Phoenix.Common.Enums.UserAgent.Web.ToString());
        //   // httpRequestMessage.Headers.Add("USR_LANG", $"{Phoenix.Common.Localization.LocalizationHelper.CurrentSystemLanguageId}");
        //    var response = client.SendAsync(httpRequestMessage);
        //    if (!response.Result.IsSuccessStatusCode && response.Result.StatusCode == HttpStatusCode.Unauthorized)
        //    {
        //        string newToken = string.Empty;
        //        if(isPhoenixAPI)
        //        {
        //            newToken = RenewPhoenixAccessToken(client);
        //        }
        //        else
        //        {
        //            newToken = RenewToken(client);
        //        }

        //        if (string.IsNullOrWhiteSpace(newToken) == false)
        //        {
        //            var newHttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = new StringContent(serializedResult, Encoding.UTF8, "application/json") };
        //            newHttpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
        //            return client.SendAsync(newHttpRequestMessage);
        //        }
        //    }
        //    return response;
        //}
        //public static Task<HttpResponseMessage> PutAsJsonAsyncExt<T>(this HttpClient client, string requestUri, T value, string token = "")
        //{
        //   // token = string.IsNullOrEmpty(token) ? SessionHelper.CurrentSession.Token : token;
        //    var serializer = new JavaScriptSerializer();
        //    var serializedResult = serializer.Serialize(value);
        //    var httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri) { Content = new StringContent(serializedResult, Encoding.UTF8, "application/json") };
        //    //httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //   // httpRequestMessage.Headers.Add("USR_LANG", $"{Phoenix.Common.Localization.LocalizationHelper.CurrentSystemLanguageId}");
        //    var response = client.SendAsync(httpRequestMessage);
        //    if (!response.Result.IsSuccessStatusCode && response.Result.StatusCode == HttpStatusCode.Unauthorized)
        //    {
        //        var newToken = RenewToken(client);
        //        if (string.IsNullOrWhiteSpace(newToken) == false)
        //        {
        //            var newHttpRequestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri) { Content = new StringContent(serializedResult, Encoding.UTF8, "application/json") };
        //            newHttpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
        //            return client.SendAsync(newHttpRequestMessage);
        //        }
        //    }
        //    return response;
        //}
        //public static Task<HttpResponseMessage> DeleteAsyncExt(this HttpClient client, string requestUri, string token = "")
        //{
        //    //token = string.IsNullOrEmpty(token) ? SessionHelper.CurrentSession.Token : token;
        //    var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        //   // httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //   // httpRequestMessage.Headers.Add("USR_LANG", $"{Phoenix.Common.Localization.LocalizationHelper.CurrentSystemLanguageId}");
        //    var response = client.SendAsync(httpRequestMessage);
        //    if (!response.Result.IsSuccessStatusCode && response.Result.StatusCode == HttpStatusCode.Unauthorized)
        //    {
        //        var newToken = RenewToken(client);
        //        if (string.IsNullOrWhiteSpace(newToken) == false)
        //        {
        //            var newHttpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        //            newHttpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
        //            return client.SendAsync(newHttpRequestMessage);
        //        }
        //    }
        //    return response;
        //}

        //public static Task<HttpResponseMessage> PostAsJsonAsyncIntegration<T>(this HttpClient client, string requestUri, T value, string token = "")
        //{
        //   // token = string.IsNullOrEmpty(token) ? SessionHelper.CurrentSession.Token : token;
        //    var serializedResult = JsonConvert.SerializeObject(value);
        //    var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = new StringContent(serializedResult, Encoding.UTF8, "application/json") };
        //    //httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    //httpRequestMessage.Headers.Add("X-Client-Agent", Phoenix.Common.Enums.UserAgent.Web.ToString());
        //    //httpRequestMessage.Headers.Add("USR_LANG", $"{Phoenix.Common.Localization.LocalizationHelper.CurrentSystemLanguageId}");
        //    var response = client.SendAsync(httpRequestMessage);
        //    if (!response.Result.IsSuccessStatusCode && response.Result.StatusCode == HttpStatusCode.Unauthorized)
        //    {
        //        var newToken = RenewToken(client);
        //        if (string.IsNullOrWhiteSpace(newToken) == false)
        //        {
        //            var newHttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = new StringContent(serializedResult, Encoding.UTF8, "application/json") };
        //            newHttpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
        //            return client.SendAsync(newHttpRequestMessage);
        //        }
        //    }
        //    return response;
        //}

        //public static Task<HttpResponseMessage> PostAsJsonAsyncDynamic<dynamic>(this HttpClient client, string requestUri, dynamic value, string token = "")
        //{
        //    //token = string.IsNullOrEmpty(token) ? SessionHelper.CurrentSession.Token : token;
        //    var serializedResult = JsonConvert.SerializeObject(value);
        //    var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = new StringContent(serializedResult, Encoding.UTF8, "application/json") };
        //    //httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    //httpRequestMessage.Headers.Add("X-Client-Agent", Phoenix.Common.Enums.UserAgent.Web.ToString());
        //    //httpRequestMessage.Headers.Add("USR_LANG", $"{Phoenix.Common.Localization.LocalizationHelper.CurrentSystemLanguageId}");
        //    var response = client.SendAsync(httpRequestMessage);
        //    if (!response.Result.IsSuccessStatusCode && response.Result.StatusCode == HttpStatusCode.Unauthorized)
        //    {
        //        var newToken = RenewToken(client);
        //        if (string.IsNullOrWhiteSpace(newToken) == false)
        //        {
        //            var newHttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = new StringContent(serializedResult, Encoding.UTF8, "application/json") };
        //            newHttpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
        //            return client.SendAsync(newHttpRequestMessage);
        //        }
        //    }
        //    return response;
        //}
      
    }
}
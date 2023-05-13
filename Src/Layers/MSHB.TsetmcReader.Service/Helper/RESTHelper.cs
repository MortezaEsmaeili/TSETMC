using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.TsetmcReader.Service.Helper
{
    public static class RESTHelper
    {
        public static T CallGetMethod<T>(string Url, out string error)
        {
            error = string.Empty;
            try
            {
                var restClient = new RestClient(Url);
                var restRequest = new RestRequest(Method.GET);
                var response = restClient.Execute(restRequest);
                string resp = response.Content.Replace("angular.callbacks._1(", "");
                resp = resp.Remove(resp.Length - 1);
                return JsonConvert.DeserializeObject<T>(resp);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return default;
            }
        }

        public static string CallGetMethod(string Url)
        {
            try
            {
                var restClient = new RestClient(Url);
                var restRequest = new RestRequest(Method.GET);
                return restClient.Execute(restRequest).Content.Trim();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}

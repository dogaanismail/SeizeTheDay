using Newtonsoft.Json;
using RestSharp;
using SeizeTheDay.Core.Constants;

namespace SeizeTheDay.Web.ServiceManager
{
    public class RestSharpManager
    {
        private const string ServerIp = ApiUrlConstants.BaseUrl;

        protected static RestClient Client;
        protected static object LockSync = new object();

        public static void Init()
        {
            lock (LockSync)
            {
                if (Client != null) return;
                Client = new RestClient(ServerIp);
                //var req = new RestRequest("values/Init", Method.GET);
                //Client.Execute(req);
                //var statu = Client.Execute<Dictionary<string, object>>(req);
                //var value = statu.Data.FirstOrDefault(p => p.Key == "Result").Value.ToString();
            }
        }

        public static TResponse RestSharpGet<TResponse>(string url)
        {
            var req = new RestRequest(url, Method.GET);
            //req.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var response = Client.Execute(req);
            return JsonConvert.DeserializeObject<TResponse>(response.Content);
        }

        public static TResponse RestSharpPost<TResponse>(string url, object data, string contentType = "application/json; charset=utf-8", bool serializeToJson = true, params Parameter[] parameters)
        {
            if (serializeToJson)
            {
                data = JsonConvert.SerializeObject(data);
            }
            var req = new RestRequest(url, Method.POST);
            req.AddParameter("Content-Type", contentType, ParameterType.HttpHeader);
            req.AddParameter(contentType, data, ParameterType.RequestBody);
            req.RequestFormat = DataFormat.Json;
            var response = Client.Execute(req);
            return JsonConvert.DeserializeObject<TResponse>(response.Content);
        }

        /// <summary>
        /// This is a generic service manager.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="method"></param>
        /// <param name="type"></param>
        /// <param name="serializeToJson"></param>
        /// <param name="deSerializeToJson"></param>
        /// <param name="contentType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static T Call<T>(
            string url, 
            object data = null, 
            Method method = Method.GET, 
            DataFormat type = DataFormat.Json, 
            bool serializeToJson = true,
            bool deSerializeToJson = true,
            string contentType = "application/json; charset=utf-8", 
            params Parameter[] parameters
            ) where T : new()
        {
            var request = new RestRequest(url, method);

            switch (method)
            {
                case Method.GET:
                    break;
                case Method.POST:
                    if (serializeToJson)
                    {
                        data = JsonConvert.SerializeObject(data);
                    }
                    request.AddParameter("Content-Type", contentType, ParameterType.HttpHeader);
                    request.AddParameter(contentType, data, ParameterType.RequestBody);
                    request.RequestFormat = type;
                    break;
                case Method.PUT:
                    break;
                case Method.DELETE:
                    break;
                case Method.HEAD:
                    break;
                case Method.OPTIONS:
                    break;
                case Method.PATCH:
                    break;
                case Method.MERGE:
                    break;
                case Method.COPY:
                    break;
                default:
                    break;
            }

            foreach (var parameter in parameters)
                request.AddParameter(parameter);

            var response = Client.Execute<T>(request);

            if (!string.IsNullOrEmpty(response.ErrorMessage) || response.ErrorException != null)
                throw new System.Exception(response.ErrorMessage, response.ErrorException);

            if (deSerializeToJson)     
                return JsonConvert.DeserializeObject<T>(response.Content);
            else
                return response.Data;
        }

        public static TResponse RestSharpPost<TResponse>(string url, object data)
        {
            var jsonToSend = JsonConvert.SerializeObject(data);
            var req = new RestRequest(url, Method.POST);
            req.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            req.RequestFormat = DataFormat.Json;
            var response = Client.Execute(req);
            return JsonConvert.DeserializeObject<TResponse>(response.Content);
        }
    }
}
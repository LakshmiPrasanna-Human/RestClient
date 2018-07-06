using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading;
using System.Net.Http.Formatting;

namespace RestClient.HttpClientWrapper
{ 
    public class RestClient<T> where T : class
    {
        //Do not Delete commented Code in this file as it is meant for future use.
        private readonly string _baseAddress;
        private readonly string _accessToken;
        public RestClient() { }
        // public RestClient(string baseAddress, string accessToken) : this(baseAddress, accessToken, false) { }
        public RestClient(string baseAddress, string accessToken)
        {
            // e.g. http://localhost/ServiceTier/api/
            _baseAddress = baseAddress;
            _accessToken = accessToken;

        }

        public async Task<T> GetSingleItemRequest(HttpClient client, string apiUrl)
        {
            T result = null;

            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _accessToken);
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
            }
            //if (client.DefaultRequestHeaders.Authorization == null)
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T>(x.Result);
                });

            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;
                    var Errorresult = JsonConvert.DeserializeObject<ResultEntity>(x.Result);
                    throw new Exception(Errorresult.ErrorDetails.ErrorMessage);
                });
            }

            return result;
        }

        public async Task<T[]> GetMultipleItemsRequest(HttpClient client, string apiUrl)
        {
            T[] result = null;

            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _accessToken);
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
            }
            //if (client.DefaultRequestHeaders.Authorization == null)
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T[]>(x.Result);
                });

            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;
                    var resultentity = JsonConvert.DeserializeObject<ResultEntity>(x.Result);
                    throw new Exception(resultentity.ErrorDetails.ErrorMessage);
                });
            }

            return result;
        }



        public async Task<T> PostRequest(HttpClient client, string apiUrl, T postObject)
        {
            T result = null;

            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _accessToken);
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
            }
            //if (client.DefaultRequestHeaders.Authorization == null)
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            var response = await client.PostAsync(apiUrl, postObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T>(x.Result);

                });

            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;
                    var Errorresult = JsonConvert.DeserializeObject<ResultEntity>(x.Result);
                    throw new Exception(Errorresult.ErrorDetails.ErrorMessage);
                });
            }

            return result;
        }

        public async Task<T> PutRequest(HttpClient client, string apiUrl, T putObject)
        {
            T result = null;
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _accessToken);
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
            }
            //if (client.DefaultRequestHeaders.Authorization == null)
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            var response = await client.PutAsync(apiUrl, putObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);

            // response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T>(x.Result);

                });

            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;
                    var Errorresult = JsonConvert.DeserializeObject<ResultEntity>(x.Result);
                    throw new Exception(Errorresult.ErrorDetails.ErrorMessage);
                });
            }
            return result;
        }

        public async Task<T> DeleteRequest(HttpClient client, string apiUrl)
        {
            T result = null;
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _accessToken);
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
            }
            //if (client.DefaultRequestHeaders.Authorization == null)
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            var response = await client.DeleteAsync(apiUrl).ConfigureAwait(false);

            //response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T>(x.Result);

                });

            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;
                    var Errorresult = JsonConvert.DeserializeObject<ResultEntity>(x.Result);
                    throw new Exception(Errorresult.ErrorDetails.ErrorMessage);
                });
            }
            return result;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Timesheet.GUI.Http.Models;

namespace Timesheet.GUI.Http
{
    public class IntegrationManager
    {
        private readonly string _baseAddress;

        public IntegrationManager()
        {
            _baseAddress = GetBaseAddress();
        }

        public async Task<WebApiResponse<TOutput>> Post<TInput, TOutput>(string module, TInput input, bool throwException = false)
        {
            return await this.GetHttpResponse<TOutput, TInput>(HttpVerb.Post, module, input, throwException);
        }

        public async Task<WebApiResponse<TOutput>> Post<TInput, TOutput>(string module, TInput input, Dictionary<string, object> keys, bool throwException = false)
        {
            if (keys != null && keys.Count() > 0)
            {
                var keysMap = keys.Select(x => String.Format("{0}={1}", x.Key, x.Value));
                module += "?" + String.Join("&", keysMap);
            }
            return await this.GetHttpResponse<TOutput, TInput>(HttpVerb.Post, module, input, throwException);
        }

        public async Task<WebApiResponse<TOutput>> Get<TOutput>(string module, object key, bool throwException = false)
        {
            return await this.GetHttpResponse<TOutput, bool?>(HttpVerb.Get, module + "/" + key, null, throwException);
        }
        public async Task<WebApiResponse<TOutput>> Get<TOutput>(string module, bool throwException = false)
        {
            return await this.GetHttpResponse<TOutput, bool?>(HttpVerb.Get, module, null, throwException);
        }
        public async Task<WebApiResponse<TOutput>> Get<TOutput>(string module, Dictionary<string, object> keys, bool throwException = false)
        {
            if (keys != null && keys.Count() > 0)
            {
                var keysMap = keys.Select(x => String.Format("{0}={1}", x.Key, x.Value));
                module += "?" + String.Join("&", keysMap);
            }
            return await this.GetHttpResponse<TOutput, bool?>(HttpVerb.Get, module, null, throwException);
        }

        public async Task<WebApiResponse<TOutput>> Delete<TOutput>(string module, object key, bool throwException = false)
        {
            return await this.GetHttpResponse<TOutput, bool?>(HttpVerb.Delete, module + "/" + key, null, throwException);
        }

        public async Task<WebApiResponse<TOutput>> Put<TInput, TOutput>(string module, TInput input, object key, bool throwException = false)
        {
            return await this.GetHttpResponse<TOutput, TInput>(HttpVerb.Put, module + "/" + key, input, throwException);
        }

        #region Private

        private async Task<WebApiResponse<TOutput>> GetHttpResponse<TOutput, TInput>(HttpVerb verb, string endPoint, TInput input, bool throwException = false)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                StringContent content = null;
                // HTTP POST
                HttpResponseMessage response = null;

                switch (verb)
                {
                    case HttpVerb.Get:
                        response = await client.GetAsync(endPoint);
                        break;
                    case HttpVerb.Post:
                        content = new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json");
                        response = await client.PostAsync(endPoint, content);
                        break;
                    case HttpVerb.Put:
                        content = new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json");
                        response = await client.PutAsync(endPoint, content);
                        break;
                    case HttpVerb.Delete:
                        response = await client.DeleteAsync(endPoint);
                        break;
                    default:
                        break;

                }

                //response.EnsureSuccessStatusCode();
                string data = await response.Content.ReadAsStringAsync();
                var result = new WebApiResponse<TOutput>();
                result.IsSucceded = response.IsSuccessStatusCode;
                result.StatusCode = response.StatusCode;

                try
                {
                    var serializeOptions = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        PropertyNameCaseInsensitive = true
                    };
                    result.Response = JsonSerializer.Deserialize<TOutput>(data, serializeOptions);
                }
                catch (Exception ex)
                {
                    // ignored
                }

                if (!response.IsSuccessStatusCode)
                {
                    SerializableError err = null;
                    try
                    {
                        err = JsonSerializer.Deserialize<SerializableError>(data);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    if (err?.Any() == true)
                    {
                        var message = err.First().Value.ToString();
                        if (throwException)
                        {
                            var ex = new Exception(message);
                            foreach (var item in err)
                            {
                                if (!ex.Data.Contains(item.Key))
                                {
                                    ex.Data.Add(item.Key, item.Value);
                                }
                            }
                            throw ex;
                        }
                        else
                        {
                            result.Message = message;
                            result.Errors = new List<ErrorItem>();

                            foreach (var item in err)
                            {
                                if (result.Errors.All(x => x.Key != item.Key))
                                {
                                    result.Errors.Add(new ErrorItem
                                    {
                                        Key = item.Key,
                                        Value = Convert.ToString(item.Value)
                                    });
                                }
                            }
                        }
                    }
                }

                return result;
            }
        }

        private string GetBaseAddress()
        {
            var configPath = Path.Combine(Directory.GetCurrentDirectory(), "app.config.json");
            var JSON = File.ReadAllText(configPath);
            dynamic config = Newtonsoft.Json.JsonConvert.DeserializeObject(JSON);

            return config.baseAddress;
        }
        enum HttpVerb
        {
            Get,
            Post,
            Put,
            Delete
        }

        #endregion
    }
}

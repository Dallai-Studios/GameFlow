using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GameFlow.Plugins.Http 
{
    /// <summary>
    /// This class can be used to perform HTTP actions such as GET, POST, PUT, PATCH, and DELETE.
    /// Every single one will return a <c>Response</c> class instance. Some of then like POST, PUT, and PATCH 
    /// also requires a Request object in order to work properly. If you want to know more, please check the documentation 
    /// </summary>
    public class Http 
    {
        private HttpClient _client;
        private string _baseURL;

        public Http() => this.InitializeHttpClient();

        public Http(string BaseURL)
        {
            this.SetBaseURL(BaseURL); 
            this.InitializeHttpClient();
        }
        
        private void InitializeHttpClient() => this._client = new HttpClient();

        private void SetDefaultHeaders([CanBeNull] Dictionary<string, string> DefaultHeaders)
        {
            this._client.DefaultRequestHeaders.Clear();
            this._client.DefaultRequestHeaders.Accept.Clear();
            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (DefaultHeaders == null) return;
            foreach(KeyValuePair<string, string> entry in DefaultHeaders)
                this._client.DefaultRequestHeaders.Add(entry.Key, entry.Value);
        }

        private Uri PrepareURL(string URL) 
        {
            return new Uri(string.IsNullOrEmpty(this._baseURL) ? URL : this._baseURL + URL);
        }

        /// <summary>
        /// This method can be used to provide for the Http class a default base URL for every request.
        /// </summary>  
        /// <remarks>
        /// The base URL will be used for each new request created, so keep in mind that if you need to 
        /// use another base URL you must change the defined URL or create a new instance of the Http class
        /// </remarks>
        /// <param name="BaseURL">Define the base URL to be used.</param>
        public void SetBaseURL(string BaseURL) 
        {
            this._baseURL = BaseURL;
        }
        
        /// <summary>
        /// Performs a GET request method
        /// </summary>
        /// <param name="URL">(string) The URL to make the request</param>
        /// <param name="Headers">(Dictionary) The request headers</param>
        /// <returns>Returns a new Response instance (awaitable)</returns>
        public async Task<Response> Get(string URL, Dictionary<string, string> Headers = null)
        {
            this.SetDefaultHeaders(Headers);
            HttpResponseMessage httpResponse = await this._client.GetAsync(this.PrepareURL(URL));
            Response response = new Response(httpResponse);
            await response.BuildResponseContent();
            return response;
        }

        /// <summary>
        /// Performs a GET request method
        /// </summary>
        /// <param name="URL">(string) The URL to make the request</param>
        /// <param name="Request">(Request) The request to send</param>
        /// <returns>Returns a new Response instance (awaitable)</returns>
        public async Task<Response> Post(string URL, Request Request) 
        {
            this.SetDefaultHeaders(Request.Headers);
            HttpResponseMessage httpResponse = await this._client.PostAsync(this.PrepareURL(URL), Request.Body);
            Response response = new Response(httpResponse);
            await response.BuildResponseContent();
            return response;
        }

        /// <summary>
        /// Performs a GET request method
        /// </summary>
        /// <param name="URL">(string) The URL to make the request</param>
        /// <param name="Request">(Request) The request to send</param>
        /// <returns>Returns a new Response instance (awaitable)</returns>
        public async Task<Response> Put(string URL, Request Request)
        {
            this.SetDefaultHeaders(Request.Headers);
            HttpResponseMessage httpResponse = await this._client.PutAsync(this.PrepareURL(URL), Request.Body);
            Response response = new Response(httpResponse);
            await response.BuildResponseContent();
            return response;
        }

        /// <summary>
        /// Performs a GET request method
        /// </summary>
        /// <param name="URL">(string) The URL to make the request</param>
        /// <param name="Request">(Request) The request to send</param>
        /// <returns>Returns a new Response instance (awaitable)</returns>
        public async Task<Response> Patch(string URL, Request Request)
        {
            this.SetDefaultHeaders(Request.Headers);
            HttpResponseMessage httpResponse = await this._client.PatchAsync(this.PrepareURL(URL), Request.Body);
            Response response = new Response(httpResponse);
            await response.BuildResponseContent();
            return response;
        }

        /// <summary>
        /// Performs a GET request method
        /// </summary>
        /// <param name="URL">(string) The URL to make the request</param>
        /// <param name="Headers">(Dictionary) The request headers</param>
        /// <returns>Returns a new Response instance (awaitable)</returns>
        public async Task<Response> Delete(string URL, Dictionary<string, string> Headers = null)
        {
            this.SetDefaultHeaders(Headers);
            HttpResponseMessage httpResponse = await this._client.DeleteAsync(this.PrepareURL(URL));
            Response response = new Response(httpResponse);
            await response.BuildResponseContent();
            return response;
        }
    }
}
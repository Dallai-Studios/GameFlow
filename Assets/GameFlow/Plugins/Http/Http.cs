using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GameFlow.Plugins.Http 
{
    /// <summary>
    /// This class can be used to perform HTTP actions such as GET, POST, PUT, PATCH, and DELETE.
    /// Every single one will return a <c>Response</c> class instance. Some of then like POST, PUT, and PATCH 
    /// also requires a Request object in order to work properly. If you want to know more, please check the documentation 
    /// </summary>
    public class Http 
    {
        #region "Private Properties"
            private HttpClient _client;
            public string _baseURL;
        #endregion
        
        #region "Private Methods"
            private void InitializeHttpClient()
            {
                this._client = new HttpClient();
            }

            private void SetDefaultHeaders()
            {
                this._client.DefaultRequestHeaders.Clear();
                this._client.DefaultRequestHeaders.Accept.Clear();
                this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            private void SetDefaultHeaders(Dictionary<string, string> DefaultHeaders)
            {
                this._client.DefaultRequestHeaders.Clear();
                this._client.DefaultRequestHeaders.Accept.Clear();
                this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if(DefaultHeaders is not null)
                {
                    foreach(KeyValuePair<string, string> entry in DefaultHeaders)
                        this._client.DefaultRequestHeaders.Add(entry.Key, entry.Value);
                }
            }

            private Uri PrepareUrl(string url) 
            {
                return new Uri(string.IsNullOrEmpty(this._baseURL) ? url : this._baseURL + url);
            } 
        #endregion

        #region "Public Methods"
            /// <summary>
            /// This method can be used to provide for the Http class a default base Url for every request.
            /// </summary>  
            /// <remarks>
            /// The base Url will be used for each new request created, so keep in mind that if you need to 
            /// use another base url you must change the defined Url or create a new instance of the Http class
            /// </remarks>
            /// <param name="BaseUrl">Define the base Url to be used.</param>
            public void SetBaseUrl(string BaseUrl) 
            {
                this._baseURL = BaseUrl;
            }
            
            /// <summary>
            /// Performs a GET request method
            /// </summary>
            /// <param name="Url">(string) The url to make the request</param>
            /// <param name="Headers">(Dictionary<string, string>) The request headers</param>
            /// <returns>Returns a new Response instance (awaitable)</returns>
            public async Task<Response> Get(string Url, Dictionary<string, string> Headers = null)
            {
                this.SetDefaultHeaders(Headers);
                HttpResponseMessage httpResponse = await this._client.GetAsync(this.PrepareUrl(Url));
                Response response = new Response(httpResponse);
                await response.BuildResponseContent();
                return response;
            }

            /// <summary>
            /// Performs a GET request method
            /// </summary>
            /// <param name="Url">(string) The url to make the request</param>
            /// <param name="Request">(Request) The request to send</param>
            /// <returns>Returns a new Response instance (awaitable)</returns>
            public async Task<Response> Post(string Url, Request Request) 
            {
                this.SetDefaultHeaders(Request.Headers);
                HttpResponseMessage httpResponse = await this._client.PostAsync(this.PrepareUrl(Url), Request.Body);
                Response response = new Response(httpResponse);
                await response.BuildResponseContent();
                return response;
            }

            /// <summary>
            /// Performs a GET request method
            /// </summary>
            /// <param name="Url">(string) The url to make the request</param>
            /// <param name="Request">(Request) The request to send</param>
            /// <returns>Returns a new Response instance (awaitable)</returns>
            public async Task<Response> Put(string url, Request Request)
            {
                this.SetDefaultHeaders(Request.Headers);
                HttpResponseMessage httpResponse = await this._client.PutAsync(this.PrepareUrl(url), Request.Body);
                Response response = new Response(httpResponse);
                await response.BuildResponseContent();
                return response;
            }

            /// <summary>
            /// Performs a GET request method
            /// </summary>
            /// <param name="Url">(string) The url to make the request</param>
            /// <param name="Request">(Request) The request to send</param>
            /// <returns>Returns a new Response instance (awaitable)</returns>
            public async Task<Response> Patch(string url, Request Request)
            {
                this.SetDefaultHeaders(Request.Headers);
                HttpResponseMessage httpResponse = await this._client.PatchAsync(this.PrepareUrl(url), Request.Body);
                Response response = new Response(httpResponse);
                await response.BuildResponseContent();
                return response;
            }

            /// <summary>
            /// Performs a GET request method
            /// </summary>
            /// <param name="Url">(string) The url to make the request</param>
            /// <param name="Headers">(Dictionary<string, string>) The request headers</param>
            /// <returns>Returns a new Response instance (awaitable)</returns>
            public async Task<Response> Delete(string url, Dictionary<string, string> Headers = null)
            {
                this.SetDefaultHeaders();
                HttpResponseMessage httpResponse = await this._client.DeleteAsync(this.PrepareUrl(url));
                Response response = new Response(httpResponse);
                await response.BuildResponseContent();
                return response;
            }
        #endregion

        #region "Constructors"
            public Http() 
            {
                this.InitializeHttpClient(); 
            }

            public Http(string BaseUrl)
            {
                this.SetBaseUrl(BaseUrl); 
                this.InitializeHttpClient();
            }
        #endregion
    }
}

using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFlow.Plugins.Http
{
    public class Response
    {
        public string Headers;
            public string Body;
            public int StatusCode;
            public bool IsSuccessStatusCode;

            private HttpResponseMessage _responseMessage;

            /// <summary>
            /// This method will abstract the response and build it. This process runs automatically
            /// </summary>
            public async Task BuildResponseContent()
            {
                this.Body = await this._responseMessage.Content.ReadAsStringAsync();
                this.Headers = this._responseMessage.Headers.ToString();
                this.StatusCode = (int) this._responseMessage.StatusCode;
                this.IsSuccessStatusCode = this._responseMessage.IsSuccessStatusCode;
            }

            /// <summary>
            /// This method will parse the response body for the provided class
            /// </summary>
            /// <returns>An instance of the provided class</returns>
            public T ParseBody<T>() where T : class
            {
                T bodyContent = JsonUtility.FromJson<T>(this.Body);
                return bodyContent;
            }

            public Response(HttpResponseMessage ResponseMessage)
            {
                this._responseMessage = ResponseMessage;
            }
    }
}


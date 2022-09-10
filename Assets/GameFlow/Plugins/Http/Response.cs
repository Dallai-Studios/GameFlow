using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFlow.Plugins.Http
{
    public class Response
    {
        private string _headers;
        private string _body;
        private int _statusCode;

        private readonly HttpResponseMessage _responseMessage;
        
        public Response(HttpResponseMessage ResponseMessage) => this._responseMessage = ResponseMessage;

        /// <summary>
        /// This method will abstract the response and build it. This process runs automatically
        /// </summary>
        public async Task BuildResponseContent()
        {
            this._body = await this._responseMessage.Content.ReadAsStringAsync();
            this._headers = this._responseMessage.Headers.ToString();
            this._statusCode = (int) this._responseMessage.StatusCode;
        }

        /// <summary>
        /// This method will parse the response body for the provided class
        /// </summary>
        /// <returns>An instance of the provided class</returns>
        public T ParseBody<T>() => JsonUtility.FromJson<T>(this._body);
    }
}


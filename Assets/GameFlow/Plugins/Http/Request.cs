using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using UnityEngine;

namespace GameFlow.Plugins.Http
{
    public class Request
    {
        public Dictionary<string, string> Headers { get; private set; }
        public StringContent Body { get; private set; }

        public Request() => this.Headers = new Dictionary<string, string>();
        
        /// <summary>
        /// Define the request default headers
        /// </summary>
        /// <param name="RequestHeaders">A Dictionary with the header keys (string) and values (string)</param>
        public void SetRequestHeaders(Dictionary<string, string> RequestHeaders) => this.Headers = RequestHeaders;

        /// <summary>
        /// Define the request body payload
        /// </summary>
        /// <param name="BodyContent"></param>
        /// <typeparam name="T"></typeparam>
        public void SetRequestBody<T>(T BodyContent)
        {
            string stringedContent = JsonUtility.ToJson(BodyContent);
            this.Body = new StringContent(stringedContent, Encoding.UTF8, "application/json");
        }
    }
}
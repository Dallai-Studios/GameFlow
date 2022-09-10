using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using UnityEngine;

namespace GameFlow.Plugins.Http
{
    public class Request
    {
        #region "Public Properties"
            public Dictionary<string, string> Headers { get; set; }
            public StringContent Body { get; set; }
        #endregion

        #region "Public Methods"
            public void SetRequestHeaders(Dictionary<string, string> RequestHeaders)
            {
                this.Headers = RequestHeaders;
            }

            public void SetRequestBody<T>(T BodyContent)
            {
                string stringfyedContent = JsonUtility.ToJson(BodyContent);
                this.Body = new StringContent(stringfyedContent, Encoding.UTF8, "application/json");
            }
        #endregion

        #region "Constructors"
            public Request() {}
        #endregion
    }
}


using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Logger.BusinessLogic
{
    public class HTTPLogger : MyLogger
    {
        private readonly string url;

        public HTTPLogger(string url)
        {
            this.url = url;
        }

        public override async void Log(int level, string message)
        {
            base.Log(level, message);

            using (var client = new HttpClient())
            {
                HttpContent content = new StringContent(Message);

                HttpResponseMessage response = await client.PostAsync(url, content);

                await response.Content.ReadAsStringAsync();
                
            }
        }
    }
}
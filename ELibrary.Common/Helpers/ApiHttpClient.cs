using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Common.Helpers
{
    public class ApiHttpClient
    {
        public HttpClient Client {get; set;}

        public ApiHttpClient() : this("")
        {

        }
        public ApiHttpClient(string token)
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public Task PostAsync(string url, object encodedContent)
        {
            throw new NotImplementedException();
        }
    }

}

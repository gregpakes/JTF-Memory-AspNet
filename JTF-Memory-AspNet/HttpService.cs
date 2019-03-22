using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace JTF_Memory_AspNet
{
    internal class HttpService : IDisposable
    {
        private HttpClient _client;
        public HttpService()
        {
            this._client = new HttpClient();
        }

        public HttpResponseMessage Get(string path)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, path);
            return this._client.SendAsync(request).Result;
        }


        public async Task<HttpResponseMessage> GetAsync(string path)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, path))
            {
                return await this._client.SendAsync(request);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Cleanup
                if (this._client != null)
                {
                    this._client.Dispose();
                }
            }
        }
        
    }
}
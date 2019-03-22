using Microsoft.VisualStudio.Threading;
using System;
using System.IO;
using System.Threading.Tasks;

namespace JTF_Memory_AspNet
{
    internal class FileService : IDisposable
    {
        private HttpService _httpService;

        public FileService(HttpService httpService)
        {
            this._httpService = httpService;
        }

        internal Stream Download()
        {
            var response = this._httpService.Get("https://raw.githubusercontent.com/gregpakes/JTF-Memory-AspNet/master/JTF-Memory-AspNet/LargeTextFile.txt");
            return response.Content.ReadAsStreamAsync().Result;
        }

        internal Stream DownloadSyncOverAsync()
        {
            var jtf = new JoinableTaskFactory(new JoinableTaskContext());
            return jtf.Run(async () => await this.DownloadAsync());
        }

        private async Task<Stream> DownloadAsync()
        {
            var response = await this._httpService.GetAsync("https://raw.githubusercontent.com/gregpakes/JTF-Memory-AspNet/master/JTF-Memory-AspNet/LargeTextFile.txt");
            return await response.Content.ReadAsStreamAsync();
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
                if (this._httpService != null)
                {
                    this._httpService.Dispose();
                }
            }
        }
    }
}
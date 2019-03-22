namespace JTF_Memory_AspNet.Hub
{
    using Microsoft.AspNet.SignalR;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;

    public class MemoryHub : Hub
    {
        private static readonly System.Timers.Timer _timer = new System.Timers.Timer();

        private static PerformanceCounter perfCounter = new PerformanceCounter();

        private static IHubContext HubContext;

        static MemoryHub()
        {
            _timer.Interval = 1000;
            _timer.Elapsed += TimerElapsed;
            _timer.Start();

            perfCounter.CategoryName = "Process";
            perfCounter.CounterName = "Private Bytes";
            perfCounter.InstanceName = Process.GetCurrentProcess().ProcessName;
        }

        static void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (HubContext == null)
            {
                HubContext = GlobalHost.ConnectionManager.GetHubContext<MemoryHub>();
            }
            HubContext.Clients.All.displayMemory(GetPrivateBytesUsed());
        }

        private static double GetPrivateBytesUsed()
        {          
            return (double)perfCounter.NextValue() / 1024 / 1024;
        }

        public void TriggerGC()
        {
            GC.Collect();
        }

        public void StartProcess(bool leaky = true)
        {
            using (var httpService = new HttpService())
            using (var fileService = new FileService(httpService))
            {
                for (var i = 0; i < 100; i++)
                {
                    var arr = new List<byte[]>();
                    if (leaky)
                    {
                        using (var filedata = fileService.DownloadSyncOverAsync())
                        using (var ms = new MemoryStream())
                        {
                            filedata.CopyTo(ms);
                            arr.Add(ms.ToArray());
                        }
                    }
                    else
                    {
                        using (var filedata = fileService.Download())
                        using (var ms = new MemoryStream())
                        {
                            filedata.CopyTo(ms);
                            arr.Add(ms.ToArray());
                        }
                    }

                    Clients.All.displayData($"Executing {i+1} of 100");
                }
            }
        }
    }
}
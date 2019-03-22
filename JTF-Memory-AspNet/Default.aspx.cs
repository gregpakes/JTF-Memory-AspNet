using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JTF_Memory_AspNet
{
    public partial class _Default : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            //using(var httpService = new HttpService())
            //using(var fileService = new FileService(httpService))
            //{                
            //    for (var i = 0; i < 100; i++)
            //    {
            //        var arr = new List<byte[]>();
            //        Console.WriteLine(i);
            //        using (var filedata = await fileService.DownloadAsync())
            //        using (var ms = new MemoryStream())
            //        {
            //            filedata.CopyTo(ms);
            //            arr.Add(ms.ToArray());
            //        }
            //    }
            //}
        }
    }
}
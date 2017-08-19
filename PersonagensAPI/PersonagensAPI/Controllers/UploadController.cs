using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PersonagensAPI.Controllers
{
    public class UploadController : ApiController
    {
        [Route("api/Files/Upload")]

        public async Task<string> Post()
        {
            try
            {
                var httpResquest = HttpContext.Current.Request;

                if (httpResquest.Files.Count > 0)
                {
                    foreach (string file in httpResquest.Files)
                    {
                        var postedFile = httpResquest.Files[file];

                        var fileName = postedFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();

                        var filePath = HttpContext.Current.Server.MapPath("~/ImageUploads/" + fileName);

                        postedFile.SaveAs(filePath);

                        return "/ImageUploads/" + fileName;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonagensApp
{
    public static class Constants
    {
        public static string RestUrl = $"http://{apiDomain}/api/Personagens";

        public static string UploadService = $"http://{apiDomain}/api/Files/Upload";

        public static string ShowLoadingMessage = "Carregando";
    }
}

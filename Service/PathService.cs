using System.IO;
using System.Web;

using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Service
{
    public class PathService : IPathService
    {
        public string GetResultFilePath(string codeFilePath)
        {
            var codeFileName = Path.GetFileNameWithoutExtension(codeFilePath);
            var codeFileDirectory = Path.GetDirectoryName(codeFilePath);

            return codeFileDirectory + "\\" + codeFileName + "_result.txt";
        }

        public string GetErrorFilePath(string codeFilePath)
        {
            var codeFileName = Path.GetFileNameWithoutExtension(codeFilePath);
            var codeFileDirectory = Path.GetDirectoryName(codeFilePath);

            return codeFileDirectory + "\\" + codeFileName + "_error.txt";
        }

        public string GetAppDataPath()
        {
            return HttpContext.Current.Server.MapPath("~/App_Data");
        }

        public string GetLocalErrorFilePath(string codeFilePath)
        {
            var codeFileName = Path.GetFileNameWithoutExtension(codeFilePath);
            var codeFileDirectory = Path.GetDirectoryName(codeFilePath);

            return codeFileDirectory + "\\" + codeFileName + "_error.txt";
        }
    }
}

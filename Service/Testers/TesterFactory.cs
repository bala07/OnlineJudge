using System;
using System.IO;

namespace OnlineJudge.Service.Testers
{
    public class TesterFactory
    {
        public static ITester GetTester(string codeFilePath)
        {
            var codeFileName = Path.GetFileNameWithoutExtension(codeFilePath);
            
            var testerType = "OnlineJudge.Service.Testers." + codeFileName + "Tester";

            return (ITester)Activator.CreateInstance(Type.GetType(testerType));
        }
    }
}

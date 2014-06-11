using System;
using System.IO;

namespace OnlineJudge.Service.Testers
{
    public class TesterFactory
    {
        public static ITester GetTester(string problemCode)
        {
            var testerType = "OnlineJudge.Service.Testers." + problemCode + "_TESTER";

            var type = Type.GetType(testerType);

            if (type == null)
            {
                throw new Exception("Tester type not valid!");
            }

            return (ITester)Activator.CreateInstance(type);
        }
    }
}

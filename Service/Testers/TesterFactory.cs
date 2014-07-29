using StructureMap;

namespace OnlineJudge.Service.Testers
{
    public class TesterFactory
    {
        public static ITester GetTester(string problemCode)
        {
            return ObjectFactory.GetInstance<Tester>();
        }
    }
}

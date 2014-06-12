namespace OnlineJudge.Service.Testers
{
    public class TesterFactory
    {
        public static ITester GetTester(string problemCode)
        {
            return new Tester();
        }
    }
}

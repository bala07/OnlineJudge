namespace OnlineJudge.Service.Testers
{
    public interface ITester
    {
        void Test(string codeFilePath, string problemCode);
    }
}

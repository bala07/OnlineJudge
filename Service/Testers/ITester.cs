using Domain.Models;

namespace OnlineJudge.Service.Testers
{
    public interface ITester
    {
        void Test(string codeFilePath, TestSuite testSuite);
    }
}

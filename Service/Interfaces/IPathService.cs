namespace OnlineJudge.Service.Interfaces
{
    public interface IPathService
    {
        string GetResultFilePath(string codeFilePath);

        string GetTesterFilePath(string codeFilePath);

        string GetErrorFilePath(string codeFilePath);
    }
}

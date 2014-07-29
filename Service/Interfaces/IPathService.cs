namespace OnlineJudge.Service.Interfaces
{
    public interface IPathService : IService
    {
        string GetResultFilePath(string codeFilePath);

        string GetTesterFilePath(string codeFilePath);

        string GetErrorFilePath(string codeFilePath);

        string GetAppDataPath();
    }
}

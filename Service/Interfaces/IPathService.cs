namespace OnlineJudge.Service.Interfaces
{
    public interface IPathService
    {
        string GetResultFilePath(string codeFilePath);

        string GetTesterFilePath(string codeFilePath);

        string GetCompilationErrorFilePath(string codeFilePath);

        string GetRuntimeErrorFilePath(string codeFilePath);

        string GetErrorFilePath(string codeFilePath);
    }
}

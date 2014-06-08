namespace OnlineJudge.Service
{
    public class Utilities
    {
        public static string GetLangaugeNameFromExtension(string extension)
        {
            switch (extension)
            {
                case ".java":
                    return "Java";
                case ".cs":
                    return "CSharp";
                default:
                    return "Unknown";
            }
        }
    }
}

namespace OnlineJudge.Service
{
    public class Utilities
    {
        // TODO: Can it be refactored to be non-static methods??
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

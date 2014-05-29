namespace Domain.Models
{
    public enum ExecutionResult
    {
        CorrectAnswer = 1,
        WrongAnswer = 2,
        TimeLimitExceeded = 3,
        CompilationError = 4,
        RuntimeError = 5,
        CompilationSuccessful = 6,
        TesterError = 7
    }
}

using System;

namespace Domain.Models
{
    public class Submission
    {
        public int Id { get; set; }

        public string ProblemCode { get; set; }

        public string UserEmail { get; set; }

        public string SubmissionTimeStamp { get; set; }

        public string FileName { get; set; }

        public ExecutionResult Status { get; set; }
    }
}

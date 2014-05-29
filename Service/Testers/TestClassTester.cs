﻿using System.IO;

using Domain.Models;

namespace OnlineJudge.Service.Testers
{
    class TestClassTester : BaseTester
    {
        public override void Test(string codeFilePath, string resultFilePath)
        {
            var compilationSuccessful = this.Compile(codeFilePath);

            if (!compilationSuccessful)
            {
                this.HandleCompilationError(codeFilePath);

                return;
            }

            var executionSuccessful = this.Run(codeFilePath);

            if (executionSuccessful)
            {
                this.HandleSuccessFulExecution(codeFilePath, ExecutionResult.CorrectAnswer);
            }
            else
            {
                this.HandleRuntimeError(codeFilePath);
            }
        }
    }
}

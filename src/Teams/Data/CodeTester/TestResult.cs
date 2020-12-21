using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace Teams.Data.CodeTester
{
    public class TestResult
    {
        public TestResult(bool success,
            string output,
            TimeSpan runningTime,
            bool exceededTheMaximumTime,
            IEnumerable<Diagnostic> compilerMessages,
            string runtimeErrors
            )
        {
            Success = success;
            Output = output;
            RunningTime = runningTime;
            ExceededTheMaximumTime = exceededTheMaximumTime;
            CompilerMessages = compilerMessages;
            RuntimeErrors = runtimeErrors;
        }
        public bool Success { get; }
        public string Output { get; }
        public TimeSpan RunningTime { get; }
        public bool ExceededTheMaximumTime { get; }
        public IEnumerable<Diagnostic> CompilerMessages { get; }
        public string RuntimeErrors { get; }
    }
}

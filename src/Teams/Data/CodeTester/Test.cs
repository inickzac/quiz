using System;

namespace Teams.Data.CodeTester
{
    public class Test
    {
        public Test(string incomingData, string expectedOutgoingData, TimeSpan maximumExecutionTime)
        {
            IncomingData = incomingData;
            ExpectedOutgoingData = expectedOutgoingData;
            MaximumExecutionTime = maximumExecutionTime;
        }
        public string IncomingData { get; }
        public string ExpectedOutgoingData { get; }
        public TimeSpan MaximumExecutionTime { get; }
    }
}

namespace SerialNumber.Service.Lambda.Console
{
    using System;

    using Amazon.Lambda.Core;

    public class ConsoleLambdaLogger : ILambdaLogger
    {
        public void Log(string message)
        {
            Console.Write(message);
        }

        public void LogLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
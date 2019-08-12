using System;

namespace Autofac.Sample.Shared
{
    public class ConsoleWriterService : IConsoleWriterService
    {
        public void WriteToConsole(string message)
        {
            Console.WriteLine(message);
        }
    }
}
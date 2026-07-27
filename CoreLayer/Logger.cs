using System;
using System.Collections.Generic;
using System.Text;

using Serilog;
//using ILogger = Serilog.ILogger; // creates an alias for Serilog.ILogger, avoiding potential conflicts
// with other ILogger interfaces (like the one in Microsoft.Extensions.Logging)

namespace CoreLayer
{
    //This code implements a wrapper class around the popular Serilog logging library
    public class Logger
    {
        private static readonly ILogger _logger = new LoggerConfiguration() //static readonly: the logger is created once and shared across all instances of the Logger class

            .WriteTo.Console()
            .CreateLogger();

        public void Information(string message)
        {
            _logger.Information(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }
    }
}

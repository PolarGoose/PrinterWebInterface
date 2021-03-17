using PrinterWebInterface.Logging;
using System;
using System.Diagnostics;
using System.Linq;

namespace PrinterWebInterface.Utils
{
    public static class CommandLine
    {
        private static ProcessStartInfo CreateProcessInfo(string command)
        {
            var parts = command.Split(" ");
            var fileName = parts.First();
            var arguments = parts.Skip(1).ToList();

            return new ProcessStartInfo
            {
                Arguments = string.Join(' ', arguments),
                FileName = fileName,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
        }

        private static (string output, string errorOutput, int exitCode) TryExecute(string command)
        {
            var process = new Process { StartInfo = CreateProcessInfo(command) };
            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
            process.WaitForExit();
            return (output, error, process.ExitCode);
        }

        private static (string output, string errorOutput) Execute(string command, bool ignoreExitCode)
        {
            Log.Info($"Execute command: '{command}'");
            var (output, errorOutput, exitCode) = TryExecute(command);
            if (exitCode != 0 && !ignoreExitCode)
            {
                throw new ApplicationException($"Command '{command}' failed with exitCode={exitCode}. Error output: [{errorOutput}]");
            }

            return (output, errorOutput);
        }

        public static string ExecuteQuery(string command, bool ignoreExitCode = false)
        {
            var (output, _) = Execute(command, ignoreExitCode: ignoreExitCode);
            return output;
        }

        public static void ExecuteCommand(string command, bool ignoreExitCode = false)
        {
            var (output, errorOutput) = Execute(command, ignoreExitCode);
            if (output != "")
            {
                Log.Info($"Output: [{output}]");
            }
            if (errorOutput != "")
            {
                Log.Info($"Error output: [{errorOutput}]");
            }
        }
    }
}

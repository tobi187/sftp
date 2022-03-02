using System;
using System.Linq;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System.IO;
using PowerArgs;

namespace sftpTest
{
    [TabCompletion]
    [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
    public class ParseArgs
    {
        [ArgShortcut(ArgShortcutPolicy.NoShortcut),
            ArgRequired,
            ArgDescription("Adresse des Server"),
            ArgPosition(0)]
        public string? Server { get; set; }

        [ArgRequired(PromptIfMissing =true),
            ArgShortcut("-x")]
        public string? Protokoll { get; set; }
        [ArgRequired]
        public string path { get; set; }

        [ArgRequired]
        [ArgShortcut("-s")]
        public string Pattern { get; set; }
        [ArgShortcut("-a")]
        public string BasicAuth { get; set; }
        [ArgShortcut("-k")]
        public string KeyFile { get; set; }
        public int Port { get; set; }
        public bool Test { get; set; } 

    }

    public static class SftpTests
    {
        static void Main(string[] args)
        {
            /*ParseArgs command;
            try
            {
                command = Args.Parse<ParseArgs>(args);
            }
            catch (ArgException e) { throw new ArgException(e.Message); }*/

            sftpConnection connection = new sftpConnection();
            /*if (!connection.areArgsValid(command))
                return;*/

            connection.DoStuff("txt", "tobi:nikelef123", "127.0.0.1", true);

        }
    }
}
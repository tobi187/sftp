using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System.IO;


namespace sftpTest
{
    internal class sftpConnection
    {
        private readonly string _host = "127.0.0.1";
        private readonly string _userName = "tobi";
        private readonly string _password = "nikelef123";

        internal void DoStuff(string ending, string auth, string host, bool isTest)
        {
            string[] authData = auth.Split(":");
            string userName = authData[0];
            string password = authData[1];

            using (SftpClient client = new SftpClient(host, userName, password))
            {
                try
                {
                    client.Connect();
                    var files = client.ListDirectory("/TestData/");
                    foreach (var file in files) {
                        var parts = file.Name.Split(".");
                        if (parts.Last() == ending)
                        {
                            var currDate = DateTime.Now.AddDays(-3);
                            if (file.LastWriteTime < currDate)
                            {
                                if (isTest)
                                {
                                    Console.WriteLine(file.Name);
                                }
                                else
                                {
                                    file.Delete();
                                }
                            }
                        } 
                    }
                } catch ( Exception e )
                { Console.WriteLine(e.ToString()); }
            }
        }

        internal bool areArgsValid(ParseArgs args)
        {
            if (args.Protokoll != "sftp" && args.Protokoll != "ftp")
            {
                Console.Write("Provide Valid Protokoll");
                return false;
            }

            return true;
        }

        internal void writeTestData(string name)
        {
            string basePath = "TestData/";
            using (SftpClient client = new SftpClient(_host, _userName, _password))
            {
                try { 
                    client.Connect();
                    client.WriteAllText(basePath + name + ".txt", "Whatever");

                } catch (Exception e) { Console.WriteLine(e.ToString()); }  
            }
        }

        internal void recursiveDeletion()
        {
            string basePath = "TestData/";
            using (SftpClient client = new SftpClient(_host, _userName, _password))
            {
                try
                {
                    var directorys = new List<SftpFile>();
                    client.Connect();
                    var dirs = client.ListDirectory(basePath);
                    foreach (var entry in dirs)
                    {
                        if (entry.IsDirectory)
                        {
                            directorys.Add(entry);
                        }
                        
                    }

                }
                catch (Exception e) 
                { Console.WriteLine(e.ToString()); }
            }
        }
    }
}

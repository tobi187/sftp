using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentFTP;
using System.Net;

namespace sftpTest
{
    internal class ftpConnection
    {
        private readonly string userName;
        private readonly string password;
        private readonly string server;
        private readonly string? pathToCertificate;
        internal ftpConnection(string user, string pass, string host)
        {
            userName = user;
            password = pass;
            server = host;    
        }

        internal void DeleteFiles(string suffix, string path, int days, bool test)
        {
            //var creds = new NetworkCredential(userName, password); 

            using (var client = new FtpClient(server, 21, userName, password))
            {

                //client.AutoConnect();
                client.EncryptionMode = FtpEncryptionMode.Auto;
                client.ValidateCertificate += new FtpSslValidation(OnValidateCertificate);
                //client.ValidateAnyCertificate = true;
                client.Connect();

                var files = client.GetListing(path);

                var checkDate = DateTime.Now.AddDays(days * -1);

                var counter = 0;

                foreach (var file in files)
                {
                    string endung = file.Name.Split('.').Last();
                    if (endung == suffix || suffix == "*")
                    {
                        if (file.Created < checkDate)
                        {
                            if (test)
                                Console.WriteLine("Zu Löschen: " + file.Name);
                            else
                                client.DeleteFile(file.FullName);

                            counter++;
                        }
                    }
                }

                client.Disconnect();

                if (test)
                    Console.WriteLine("Dateien zu löschen: " + counter);
                else
                    Console.WriteLine("Dateien gelöscht " + counter);

            }

            void OnValidateCertificate(FtpClient control, FtpSslValidationEventArgs e)
            {
                // add logic to test if certificate is valid here
                if (e.PolicyErrors == System.Net.Security.SslPolicyErrors.None)
                {
                    e.Certificate.GetRawCertData() ==
                    e.Accept = true;
                }
                else
                {
                    e.Accept = false;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp100
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var ipAddress = IPAddress.Parse("127.0.0.1");
            var ipEndpoint = new IPEndPoint(ipAddress, 2203);
            var tcpClient = new TcpClient();
            tcpClient.Connect(ipEndpoint);

            var tcpClientStreamReader = new StreamReader(tcpClient.GetStream());
            var tcpClientStreamWriter = new StreamWriter(tcpClient.GetStream());
            Task.Run(async () =>
            {
                var getMessage = new char[1024];
                while (true)
                {
                    await tcpClientStreamReader.ReadAsync(getMessage, 0, getMessage.Length);
                    Console.WriteLine(getMessage);
                    Array.Clear(getMessage);
                }
            });
            tcpClientStreamWriter.AutoFlush = true;
            while (true)
            {
                var getMessage = Console.ReadLine();
                await tcpClientStreamWriter.WriteAsync(getMessage);
            }
        }
    }
}

/***********************************************************************************************************************
 Copyright (c) 2016, Imagination Technologies Limited and/or its affiliated group companies.
 All rights reserved.

 Redistribution and use in source and binary forms, with or without modification, are permitted provided that the
 following conditions are met:
     1. Redistributions of source code must retain the above copyright notice, this list of conditions and the
        following disclaimer.
     2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the
        following disclaimer in the documentation and/or other materials provided with the distribution.
     3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote
        products derived from this software without specific prior written permission.

 THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
 INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
 DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
 SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE 
 USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
***********************************************************************************************************************/

using DTLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    public class Program
    {
        static string IP = "127.0.0.1";
        static byte[] KEY = new byte[] { 0x7C, 0xCD, 0xE1, 0x4A, 0x5C, 0xF3, 0xB7, 0x1C, 0x0C, 0x08, 0xC8, 0xB7, 0xF9, 0xE5 };

        public static void Main(string[] args)
        {
            bool exit = false;
            Console.WriteLine("Press any key to Connect to Server");
            Console.ReadKey(true);
            Client client = new Client(new IPEndPoint(IPAddress.Any, 56239));
            client.PSKIdentities.AddIdentity(IP, KEY);
            client.SetVersion(new Version(1, 2));

            client.SupportedCipherSuites.Add(TCipherSuite.TLS_PSK_WITH_AES_128_CCM_8);
            client.ConnectToServer(new IPEndPoint(IPAddress.Parse(IP), 5684));
            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e)
            {
                e.Cancel = true;
                exit = true;
            };
            Console.WriteLine();
            Console.WriteLine("Press Ctrl+C to stop the client. Any other characters are send to server");
            Console.WriteLine();
            while (!exit)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                    client.Send(Encoding.UTF8.GetBytes(pressedKey.KeyChar.ToString()));
                }
            }
            client.Stop();
        }
    }
}

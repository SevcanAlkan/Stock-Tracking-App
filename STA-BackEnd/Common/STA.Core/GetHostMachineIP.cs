using System;
using System.Net;
using System.Net.Sockets;

namespace STA.Core
{
    public static class GetHostMachineIP
    {
        public static string Get()
        {
            string ipAddress = "127.0.0.1";

            try
            {
                ipAddress = Dns.GetHostAddresses(new Uri("http://docker.for.win.localhost").Host)[0].ToString();
            }
            catch (SocketException)
            {
            }
            catch (Exception)
            {
            }

            return ipAddress;
        }
    }

    public interface IGetHostMachineIP
    {
        string Get();
    }
}

using SUS.MvcFramework;
using System;
using System.Threading.Tasks;

namespace SharedTrip
{
    public static class Program
    {
        public static async Task Main()
        {
            await Host.CreateHostAsync(new Startup(), 8080);
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebFormBD.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
           CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseUrls(GetURL().Result);
                });



        private static async Task<string> GetURL()
        {
            if (!File.Exists("configApi.json"))
                throw new Exception("Файл configApi.json отсутствует");

            try
            {
                var s = "configApi.json";

                using (StreamReader r = new StreamReader(s))
                {
                    string json = await r.ReadToEndAsync();
                    var config = JsonConvert.DeserializeObject<dynamic>(json);
                    return $"{config.protokol}://{config.url}:{config.port};";
                }
            }
            catch
            {
                throw new Exception("Файл configApi.json поврежден");
            }
        }
    }
}

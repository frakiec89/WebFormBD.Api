using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebFormBD.Api.EF
{
    public class SqlContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public SqlContext()
        {
      
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer
                (GetConectionString());
        }

        private string GetConectionString()
        {
            if (!File.Exists("configsql.json"))
                throw new Exception("Файл ConfigSQL.json отсутствует");
            
            try
            {
                var s = "configsql.json";

                using (StreamReader r = new StreamReader(s))
                {
                    string json = r.ReadToEnd();
                    var config = JsonConvert.DeserializeObject<dynamic>(json);
                    return $"Data Source={config.server};Initial Catalog={config.nameDataBase};" +
                          $"User Id =  {config.user};password={config.password}";
                }
            }
            catch
            {
                throw new Exception("Файл ConfigSQL.json поврежден");
            }
            
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                (@"Server=192.168.10.160;Database=WebFormBD; User ID=Sa; Password= Frakiec89");
        }
    }
}

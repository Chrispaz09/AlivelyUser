using Alively.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alively.Infrastructure.Data
{
    public class AlivelyDbContext : DbContext
    {

        public AlivelyDbContext()
            : base()
        {

        }

       protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=tcp:alivelyserver.database.windows.net,1433;Initial Catalog=AlivelyMVC_db;Persist Security Info=False;User ID=admin-sql;Password=Koenigseggone1*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public DbSet<User> Users { get; set; }
    }
}

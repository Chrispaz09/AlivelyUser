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
            builder.UseSqlServer("Server=LT-5CG1407BSF;Database=AlivelyUser;Trusted_Connection=True;");
        }

        public DbSet<User> Users { get; set; }
    }
}

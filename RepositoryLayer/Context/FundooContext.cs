using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Context
{
    public class FundooContext : DbContext
    {

        public FundooContext(DbContextOptions options) : base(options){ }
        public DbSet<UserEntity> UserTable { get; set; }

        public DbSet<Products> TProducts { get; set; }

        
    }
}

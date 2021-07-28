using FinalServer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalServer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
          : base(options)
        {
        }
        public DbSet<Employer> employers { get; set; }
        public DbSet<Jobseeker> jobseekers { get; set; }
        public DbSet<Admin> admin { get; set; }
        public DbSet<Login> login { get; set; }
        public DbSet<Jobpost> jobpost { get; set; }
        public DbSet<Files> files { get; set; }
        public DbSet<Apply> applyjobs { get; set; }
        public DbSet<map> mp { get; set; }


    }
}

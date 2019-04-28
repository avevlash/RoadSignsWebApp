using Microsoft.EntityFrameworkCore;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class MainContext:DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Sign> Signs { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
    }
}

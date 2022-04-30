using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Teltonika.DataModels;
using Teltonika.Interfaces;

namespace Teltonika.DBContext
{
    public class Context : DbContext, IContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
        public DbSet<Covid19Case>? Covid19Cases { get; set; }
        public DbSet<User>? Users { get; set; }

        public new async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (optionsBuilder.IsConfigured)
        //    {
        //        base.OnConfiguring(optionsBuilder);
        //        return;
        //    }

        //    string pathToContentRoot = Directory.GetCurrentDirectory();
        //    string json = Path.Combine(pathToContentRoot, "appsettings.json");

        //    if (!File.Exists(json))
        //    {
        //        string pathToExe = Process.GetCurrentProcess().MainModule.FileName;
        //        pathToContentRoot = Path.GetDirectoryName(pathToExe);
        //    }

        //    IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
        //        .SetBasePath(pathToContentRoot)
        //        .AddJsonFile("appsettings.json");

        //    IConfiguration configuration = configurationBuilder.Build();

        //    optionsBuilder.UseSqlServer(configuration.GetConnectionString("LocalDatabase"));

        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}

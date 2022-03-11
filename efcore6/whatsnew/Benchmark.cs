using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BenchmarkDotNet.Running;
using whatsnew;
using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace whatsnew
{
    public class Benchmark
    {        
        IHostBuilder builder;
        Microsoft.Extensions.Hosting.IHost host;
        IConfiguration config;
        ServiceCollection services;
        ServiceProvider serviceProvider;
        Func<WideWorldImportersContextNU, string, IEnumerable<Customer>> queryCustomers;
        public Benchmark()
        {
            builder = Host.CreateDefaultBuilder();
            host = builder.Build();
            // Defining my configuration system, using app secrets
            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables()
                .Build();

            // Dependency injection, i could use AddDbContextPool in a real app
            services = new ServiceCollection();
            services.AddDbContext<WideWorldImportersContext>(options =>
                options.UseSqlServer(config["ConnectionStrings:localconf"])
             //   .LogTo(Console.WriteLine).EnableSensitiveDataLogging()
               .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
             //   .EnableThreadSafetyChecks(false)
                );

            services.AddDbContext<WideWorldImportersContextNU>(options =>
                options.UseSqlServer(config["ConnectionStrings:localconf"])
             //   .LogTo(Console.WriteLine).EnableSensitiveDataLogging()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
             //   .EnableThreadSafetyChecks(false)
                );

            queryCustomers =
                EF.CompileQuery((WideWorldImportersContextNU context, string strCust) => context.Customers.TagWith("Query compiled!").Where(c => c.NuCustomerName == strCust));

             serviceProvider = services.BuildServiceProvider();
        }

        [Benchmark]
        public void QueryWithLiteralsAndScan()
        {
            // Getting a db context instance
            var context = serviceProvider.GetRequiredService<WideWorldImportersContext>();

            // var strCust = "Tailspin Toys (Head Office)";
            var cust = context.Customers.TagWith("Query with wrong data type!").Select(c => new { c.CustomerId, c.NuCustomerName }).Where(c => c.NuCustomerName == "Tailspin Toys (Biscay, MN)").ToList();
            
            foreach (var c in cust)
            {
                Console.WriteLine($"{c.CustomerId} {c.NuCustomerName}");
            }

            cust = context.Customers.TagWith("Query with wrong data type!").Select(c => new { c.CustomerId, c.NuCustomerName }).Where(c => c.NuCustomerName == "Tailspin Toys (Sylvanite, MT)").ToList();
            
            foreach (var c in cust)
            {
                Console.WriteLine($"{c.CustomerId} {c.NuCustomerName}");
            }
        }
        [Benchmark]
        public void QueryCompiled()
        {
            // Getting a db context instance
            var context = serviceProvider.GetRequiredService<WideWorldImportersContextNU>();

            var strCust = "Tailspin Toys (Head Office)";
            var cust = queryCustomers(context,strCust).Select(c => new { CustomerId = c.CustomerId, NuCustomerName = c.NuCustomerName }).ToList();

            foreach (var c in cust)
            {
                Console.WriteLine($"{c.CustomerId} {c.NuCustomerName}");
            }

        }

        [Benchmark]
        public void QueryWithParameterAndNoScan()
        {
            // Getting a db context instance
            var context = serviceProvider.GetRequiredService<WideWorldImportersContextNU>();

            var strCust = "Tailspin Toys (Head Office)";
            var cust = context.Customers.TagWith("Query with literals!").Select(c => new { c.CustomerId, c.NuCustomerName }).Where(c => c.NuCustomerName == strCust).ToList();
            
            foreach (var c in cust)
            {
                Console.WriteLine($"{c.CustomerId} {c.NuCustomerName}");
            }
            strCust = "Tailspin Toys (Sylvanite, MT)";
            cust = context.Customers.TagWith("Query with literals!").Select(c => new { c.CustomerId, c.NuCustomerName }).Where(c => c.NuCustomerName == strCust).ToList();
            
            foreach (var c in cust)
            {
                Console.WriteLine($"{c.CustomerId} {c.NuCustomerName}");
            }
        }

        [Benchmark]
        public void QueryWithClientSideProcessing()
        {
            // Getting a db context instance
            var context = serviceProvider.GetRequiredService<WideWorldImportersContextNU>();
            
            var cust = context.Customers.AsEnumerable().Where(c => ToLower(c.NuCustomerName).Contains("stonefort")).ToList();
            //var cust = context.Customers.Where(c => ToLower(c.NuCustomerName).Contains("stonefort")).ToList();
            foreach (var c in cust)
            {
                Console.WriteLine($"{c.CustomerId} {c.CustomerName}");
            }
        }

        static string ToLower(string value)
        {
            return value.ToLower();
        }

        [Benchmark]
        public void InsertWithBatches()
        {
            // Getting a db context instance
            var context = serviceProvider.GetRequiredService<WideWorldImportersContext>();
            
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE test");
            context.test.AddRange(new test[] {
                new test { id = 1, val = "test1" },
                new test { id = 2, val = "test2" },
                new test { id = 3, val = "test3" },
                new test { id = 4, val = "test4" },
                new test { id = 6, val = "test1" },
                new test { id = 7, val = "test2" },
                new test { id = 8, val = "test3" },
                new test { id = 9, val = "test4" },
                new test { id = 10, val = "test1" },
                new test { id = 11, val = "test2" },
                new test { id = 12, val = "test3" },
                new test { id = 13, val = "test4" },
                new test { id = 14, val = "test1" },
                new test { id = 15, val = "test2" },
                new test { id = 16, val = "test3" },
                new test { id = 17, val = "test4" },
                new test { id = 18, val = "test1" },
                new test { id = 19, val = "test2" },
                new test { id = 20, val = "test3" },
                new test { id = 21, val = "test4" },
                new test { id = 22, val = "test1" },
                new test { id = 23, val = "test2" },
                new test { id = 24, val = "test3" },
                new test { id = 25, val = "test4" },
                new test { id = 26, val = "test1" },
                new test { id = 27, val = "test2" },
                new test { id = 28, val = "test3" },
                new test { id = 29, val = "test4" },
                new test { id = 30, val = "test1" },
                new test { id = 31, val = "test2" },
                new test { id = 32, val = "test3" },
                new test { id = 33, val = "test4" },
                new test { id = 34, val = "test5" }
            });
            context.SaveChanges(true);
            context.ChangeTracker.Clear();
        }
    }
}
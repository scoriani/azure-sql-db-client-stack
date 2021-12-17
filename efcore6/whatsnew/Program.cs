using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using whatsnew;
// Defining a host just in case i want to use migrations later
var builder = Host.CreateDefaultBuilder(args);
using IHost host = builder.Build();
// Defining my configuration system, using app secrets
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables()
    .Build();

var services = new ServiceCollection();
// Dependency injection, i could use AddDbContextPool in a real app
services.AddDbContext<WideWorldImportersContext>(options =>
    options.UseSqlServer(config["ConnectionStrings:localconf"])
    .LogTo(Console.WriteLine).EnableSensitiveDataLogging()
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
 //   .EnableThreadSafetyChecks(false)
    );

var serviceProvider = services.BuildServiceProvider();

// Getting a db context instance
var context = serviceProvider.GetRequiredService<WideWorldImportersContext>();

using (context)
{

    // Tailspin Toys (Head Office)
    // Tailspin Toys (Sylvanite, MT)
    // Tailspin Toys (Peeples Valley, AZ)
    // Tailspin Toys (Medicine Lodge, KS)
    // Tailspin Toys (Gasport, NY)
    // Tailspin Toys (Jessie, ND)
    // Tailspin Toys (Frankewing, TN)
    // Tailspin Toys (Bow Mar, CO)
    // Tailspin Toys (Netcong, NJ)
    // Tailspin Toys (Wimbledon, ND)
    // Tailspin Toys (Devault, PA)
    // Tailspin Toys (Biscay, MN)
    // Tailspin Toys (Stonefort, IL)

    //var strCust = "Tailspin Toys (Head Office)";
    var cust = context.Customers.TagWith("This is my query!").Select(c => new { c.CustomerId, c.NuCustomerName }).Where(c => c.NuCustomerName == "Tailspin Toys (Biscay, MN)").ToList();
    
    foreach (var c in cust)
    {
        Console.WriteLine($"{c.CustomerId} {c.NuCustomerName}");
    }

    // var cust = context.Customers.AsEnumerable().Where(c => ToLower(c.NuCustomerName).Contains("stonefort")).ToList();
    // foreach (var c in cust)
    // {
    //     Console.WriteLine($"{c.CustomerId} {c.CustomerName}");
    // }

    // static string ToLower(string value)
    // {
    //     return value.ToLower();
    // }

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

    context.SaveChanges();

}


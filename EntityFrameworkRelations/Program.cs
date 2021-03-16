using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

Console.WriteLine("Hello World!");

var factory = new BrickContextFactory();
using var context = factory.CreateDbContext();
await AddData();
await QueryData();
Console.WriteLine("Done!");

async Task AddData()
{
    Vendor brickKing, heldDerSteine;
    await context.AddRangeAsync(new[]
    {
        brickKing = new Vendor() {VendorName = "Brick King"},
        heldDerSteine = new Vendor() {VendorName = "Held Der Steine"},
    });

    await context.SaveChangesAsync();

    Tag rare, ninjago, minecraft;

    await context.AddRangeAsync(new[]
    {
        rare = new Tag() {Title = "Rare"},
        ninjago = new Tag() {Title = "Ninjago"},
        minecraft = new Tag() {Title = "Minecraft"},
    });
    await context.SaveChangesAsync();

    await context.AddAsync(new BasePlate
    {
        Title = "BasePlate 16 x 16 with blue water pattern",
        Color = Color.Green,
        Tags = new() { rare, minecraft },
        Length = 16,
        Width = 16,
        Availability = new()
        {
            new() { Vendor = brickKing, AvailableAmount = 5, PriceEur = 6.5m},
            new() { Vendor = heldDerSteine, AvailableAmount = 10, PriceEur = 5.9m },
        }
    });
    await context.SaveChangesAsync();
}

async Task QueryData()
{
    var availabilityData = await context.BrickAvailabilities
        .Include(ba => ba.Brick)
        .Include(ba => ba.Vendor)
        .ToArrayAsync();
    foreach (var item in availabilityData)
    {
        Console.WriteLine($"Brick {item.Brick.Title} available at {item.Vendor.VendorName} for {item.PriceEur}");
    }

    Console.WriteLine();

    var brickWithVendorsAndTags = await context.Bricks
        .Include(nameof(Brick.Availability) + "." + nameof(BrickAvailability.Vendor))
        .Include(b => b.Tags)
        .ToArrayAsync();

    foreach (var item in brickWithVendorsAndTags)
    {
        Console.Write($"Brick {item.Title} ");
        if (item.Tags.Any()) Console.Write($"({string.Join(',', item.Tags.Select(t => t.Title))})");
        if (item.Availability.Any()) Console.WriteLine($"is available at ({string.Join(',', item.Availability.Select(t => t.Vendor.VendorName))})");
    }

    //comment Upper command/ it is loaded before so it is in memory.
    var simpleBricks = await context.Bricks.ToArrayAsync();
    foreach (var item in simpleBricks)
    {
        await context.Entry(item).Collection(i => i.Tags).LoadAsync();
        Console.Write($"Brick {item.Title} ");
        if (item.Tags.Any()) Console.Write($"({string.Join(',', item.Tags.Select(t => t.Title))})");
    }
}

#region Model

enum Color
{
    Black,
    White,
    Red,
    Yellow,
    Orange,
    Green
}

class Brick
{
    public int Id { get; set; }

    [MaxLength(250)]
    public string Title { get; set; } = string.Empty;

    public Color? Color { get; set; }

    public List<Tag> Tags { get; set; } = new();

    public List<BrickAvailability> Availability { get; set; } = new();
}

class BasePlate : Brick
{
    public int Length { get; set; }

    public int Width { get; set; }

}

class MinifigHead : Brick
{
    public bool IsDualSided { get; set; }

}
class Tag
{
    public int Id { get; set; }

    [MaxLength(250)]
    public string Title { get; set; } = string.Empty;

    public List<Brick> Bricks { get; set; }

}

class Vendor
{
    public int Id { get; set; }

    [MaxLength(250)]
    public string VendorName { get; set; }

    public List<BrickAvailability> Availability { get; set; } = new();

}

class BrickAvailability
{
    public int Id { get; set; }

    public Vendor Vendor { get; set; }

    public int VendorId { get; set; }

    public Brick Brick { get; set; }
    public int BrickId { get; set; }

    public int AvailableAmount { get; set; }

    [Column(TypeName = "decimal(8, 2)")]
    public decimal PriceEur { get; set; }

}

#endregion


#region Data Context
class BrickContext : DbContext
{
    public BrickContext(DbContextOptions<BrickContext> options) : base(options)
    {
    }

    public DbSet<Brick> Bricks { get; set; }

    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<BrickAvailability> BrickAvailabilities { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BasePlate>().HasBaseType<Brick>();
        modelBuilder.Entity<MinifigHead>().HasBaseType<Brick>();
        //base.OnModelCreating(modelBuilder);
    }
}

class BrickContextFactory : IDesignTimeDbContextFactory<BrickContext>
{
    public BrickContext CreateDbContext(string[] args = null)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var optionsBuilder = new DbContextOptionsBuilder<BrickContext>();
        optionsBuilder
            // Uncomment the following line if you want to print generated
            // SQL statements on the console.
            //.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

        return new BrickContext(optionsBuilder.Options);
    }
}

#endregion
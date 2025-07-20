using EatWise.Customers;
using EatWise.Harvester.Application.Abstractions.Data;
using EatWise.Harvester.Infrastructure.Customers;
using EatWise.Harvester.Infrastructure.Harvesters;
using EatWise.Recipes;
using Microsoft.EntityFrameworkCore;

namespace EatWise.Harvester.Infrastructure.Database;

public sealed class HarvesterDbContext(DbContextOptions<HarvesterDbContext> options): DbContext(options), IUnitOfWork
{
    internal DbSet<Recipe> Recipes { get; set; }
    internal DbSet<Customer> Customers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Harvesters);

        modelBuilder.ApplyConfiguration(new RecipeConfiguration());
        
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
    }
}

using Feladat0104.Models;
using Microsoft.EntityFrameworkCore;

namespace Feladat0104.Data;

public class FeladatDbContext : DbContext
{
    public DbSet<Actor> Actors { get; set; } = null!;
    public DbSet<Movie> Movies { get; set; } = null!;

    public FeladatDbContext(DbContextOptions<FeladatDbContext> options) : base(options)
    {
    }
}
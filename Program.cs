using Feladat0104.Data;
using Feladat0104.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FeladatDbContext>(options =>
{
    options.UseMySql(
        "server=localhost;database=fz_feladat0104;user=fz_feladat0104;password=asd123",
        MySqlServerVersion.LatestSupportedServerVersion
    );
});

builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope())
{
    using var ctx = serviceScope.ServiceProvider.GetService<FeladatDbContext>();
    ctx!.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

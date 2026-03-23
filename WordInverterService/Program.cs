using Microsoft.EntityFrameworkCore;
using WordInverterService.Data;
using WordInverterService.DataContext;
using WordInverterService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbProvider = builder.Configuration.GetValue<string>("DatabaseProvider") ?? "Sqlite";
builder.Services.AddDbContext<AppDbContext>(options =>
{
    _ = dbProvider switch
    {
        "SqlServer" => options.UseSqlServer(
            builder.Configuration.GetConnectionString("SqlServer"),
            sql => sql.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)),

        "Sqlite" => options.UseSqlite(
            builder.Configuration.GetConnectionString("Sqlite"),
            sql => sql.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)),

        _ => throw new InvalidOperationException(
            $"Unsupported DatabaseProvider '{dbProvider}'. Valid values are 'Sqlite' and 'SqlServer'.")
    };
});
builder.Services.AddScoped<IWordInversionRepository, WordInversionRepository>();
builder.Services.AddScoped<InversionService>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database migration failed — application cannot start. \nDetails: {ex}");
        throw;
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

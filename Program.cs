using AgendaContatos.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = BuildConnectionString(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();

static string BuildConnectionString(IConfiguration configuration)
{
    var configuredConnectionString = configuration.GetConnectionString("AgendaContatosDb");
    var connectionString = string.IsNullOrWhiteSpace(configuredConnectionString)
        ? CreateLocalDataConnectionString()
        : Environment.ExpandEnvironmentVariables(configuredConnectionString);

    EnsureSqliteDirectoryExists(connectionString);

    return connectionString;
}

static string CreateLocalDataConnectionString()
{
    var databaseDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "AgendaContatos");

    return new SqliteConnectionStringBuilder
    {
        DataSource = Path.Combine(databaseDirectory, "agenda-contatos.db")
    }.ConnectionString;
}

static void EnsureSqliteDirectoryExists(string connectionString)
{
    var sqliteConnectionString = new SqliteConnectionStringBuilder(connectionString);

    if (string.IsNullOrWhiteSpace(sqliteConnectionString.DataSource) ||
        sqliteConnectionString.DataSource.Equals(":memory:", StringComparison.OrdinalIgnoreCase))
    {
        return;
    }

    var databasePath = Path.IsPathRooted(sqliteConnectionString.DataSource)
        ? sqliteConnectionString.DataSource
        : Path.GetFullPath(sqliteConnectionString.DataSource);

    var databaseDirectory = Path.GetDirectoryName(databasePath);

    if (!string.IsNullOrWhiteSpace(databaseDirectory))
    {
        Directory.CreateDirectory(databaseDirectory);
    }
}
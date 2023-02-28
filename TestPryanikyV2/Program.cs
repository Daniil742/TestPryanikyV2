using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;
using TestPryanikyV2.Data.Context;
using TestPryanikyV2.Data.Interfaces;
using TestPryanikyV2.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

RegisterServices(builder.Services);

var app = builder.Build();

Configure(app);

app.Run();


void RegisterServices(IServiceCollection services)
{
    services.AddSwaggerGen(config =>
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        config.IncludeXmlComments(xmlPath);
    });

    services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

    services.AddDbContext<PryanikyDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
    });

    services.AddScoped<IUnitOfWork, UnitOfWork>();
}

void Configure(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<PryanikyDbContext>();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        //DbInitializer.Initialize(db);
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}
using agent;
using agent.MapperConfig;
using agent.TableInteraction.TableSpecificInerfaces;
using agent.TableInteraction.TableSpecificInteract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<agentDbContextSqlServer>(option =>
//    option.UseSqlServer(builder.Configuration["SQLSERVER_DOCKER_CONNECTION_STRING"])
//);

builder.Services.AddDbContext<agentDbContextSqlite>(option =>
    option.UseSqlite(builder.Configuration.GetConnectionString("agentDbConnectionSqlite"))
);

builder.Services.AddAutoMapper(typeof(configMapper));

//registering table interaction classes for dependency injectio
builder.Services.AddScoped<IAgency, AgencyRepository>();
builder.Services.AddScoped<IAgencyCountry, AgencyCountryRepository>();
builder.Services.AddScoped<IAgencyDocument, AgencyDocumentRepository>();
builder.Services.AddScoped<IAgencyReviews, AgencyReviewRepository>();
builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<ICountry, CountryRepository>();
builder.Services.AddScoped<IRegion, RegionRepository>();
builder.Services.AddScoped<IJob, JobRepository>();
builder.Services.AddScoped<IJobApplication, JobApplicationRepository>();
builder.Services.AddScoped<IJobCategory, JobCategoryRepository>();

builder.Services.AddCors(options =>
{
    //default policy - allow all origins for development
    options.AddDefaultPolicy(
        policy =>
        {
            //allow any origin,method and header
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .SetIsOriginAllowed(_ => true); // Allow all origins explicitly
        }
    );

    // Named policy for extra flexibility
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<agentDbContextSqlServer>();
//    db.Database.Migrate();

//    await DbSeeder.SeedAsync(db);
//}

app.UseSwagger();
app.UseSwaggerUI();

// Comment out HTTPS redirection for HTTP-only deployment
// app.UseHttpsRedirection(); 
app.UseStaticFiles();

app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();

app.Run();
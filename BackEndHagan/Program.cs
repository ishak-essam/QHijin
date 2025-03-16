using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Extensions;
using BackEndHagan.Mapper;
using BackEndHagan.Middleware;
using BackEndHagan.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<HaganContext> (options =>
{
    options.UseSqlServer (builder.Configuration.GetConnectionString ("DefualtConnection"));
});
builder.Services.Configure<JwtOptions> (builder.Configuration.GetSection ("ApiSettings"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole> (ele=>ele.User.RequireUniqueEmail=true).AddEntityFrameworkStores<HaganContext> ().AddDefaultTokenProviders();
builder.AddAppAuthetication ();
builder.Services.AddAuthorization ();
builder.Services.AddControllers ().AddJsonOptions (options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
}); ;
//Mapping
IMapper _mapper=MappingConfig.RegisterMap().CreateMapper();
builder.Services.AddSingleton (_mapper);
builder.Services.AddAutoMapper (AppDomain.CurrentDomain.GetAssemblies ());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddSwaggerGen ();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware> ();
app.UseCors ("CorsPolicy");
// Configure the HTTP request pipeline.
app.UseSwagger ();
app.UseSwaggerUI (c =>
{
    if ( !app.Environment.IsDevelopment () )
    {
        c.SwaggerEndpoint ("/swagger/v1/swagger.json", "HIJIN");
        c.RoutePrefix = string.Empty;
    }
});

app.UseHttpsRedirection ();
app.UseAuthentication ();
app.UseAuthorization ();
app.UseStaticFiles ();
app.MapControllers ();

app.Run ();

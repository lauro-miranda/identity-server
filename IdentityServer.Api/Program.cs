using IdentityServer.Api.Domain.Repositories.Contracts;
using IdentityServer.Api.Domain.Services;
using IdentityServer.Api.Domain.Services.Contracts;
using IdentityServer.Api.Extensions;
using IdentityServer.Api.Infra.Context;
using IdentityServer.Api.Infra.Repositories;
using IdentityServer.Api.Settings;
using LM.Domain.UnitOfWork;
using LM.Infra.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

if (builder.Environment.IsDevelopment())
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}

services.AddControllers();

services.Configure<IdentityServerSettings>(configuration.GetSection(nameof(IdentityServerSettings)));

services.AddScoped<IUnitOfWork, UnitOfWork<IdentityServerContext>>();
var serverVersion = new MySqlServerVersion(new Version(8, 0, 23));
services.AddDbContext<IdentityServerContext>(options => options.UseMySql(configuration.GetConnectionString("Me"), serverVersion));

services.AddTransient<IUserService, UserService>();
services.AddTransient<IUserRepository, UserRepository>();

services.ConfigureIdentityServer();

services.AddAuthentication(configuration);

services.ConfigureHttpClientPool(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseIdentityServer();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
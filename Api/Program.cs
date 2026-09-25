using Api.Extensions;
using Application;
using CEIS.Api.Extensions;
using CEIS.Api.Middleware;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices()
    .AddIdentityServices()
    .AddApiServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Identity Role Add
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    await RoleSeeder.SeedRolesAsync(scope.ServiceProvider);
    await CategorySeeder.SeedAsync(context);
}

app.UseStaticFiles();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("AllowReact");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

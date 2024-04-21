using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using SilliconWebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.RegisterSwagger();
builder.Services.RegisterJwt(builder.Configuration);
builder.Services.AddCors(x =>
{
    x.AddPolicy("CustomOriginPolicy", policy =>
    {
        policy
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});

builder.Services.RegisterRepositories(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

app.UseCors("CustomOriginPolicy");

app.UseSwagger();
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json","Silicon Web Api v1"));
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
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
        .WithOrigins("https://localhost:7198")
        .AllowAnyHeader()
        .AllowAnyMethod();
        //.AllowAnyOrigins();
    });
});

builder.Services.AddScoped<SubscriberRepository>();
builder.Services.AddScoped<SubscriberService>();

builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<CourseService>();

var app = builder.Build();

// allowanyheaders innebär tex att man kan använda alla olika contenttypes eller keys osv..
// allowanyorigin innebär att alla har tillgång till apiet.
// allowanymethods innebär att man kan göra alla metoder som tex post,put,delete,get osv.
// just nu har vi skrivit att bara min adress sak få komma åt detta apiet.(se högre upp)
// eftersom vi kör localhost så kommer det fungerar nu ändå.
app.UseCors("CustomOriginPolicy");

app.UseSwagger();
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json","Silicon Web Api v1"));
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
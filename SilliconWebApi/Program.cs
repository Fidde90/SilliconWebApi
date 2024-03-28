using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddScoped<SubscriberRepository>();
builder.Services.AddScoped<SubscriberService>();

builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<CourseService>();


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


var app = builder.Build();

// allowanyheaders innebär tex att man kan använda alla olika contenttypes eller keys osv..
// allowanyorigin innebär att alla har tillgång till apiet.
// allowanymethods innebär att man kan göra alla metoder som tex post,put,delete,get osv.
// just nu har vi skrivit att bara min adress sak få komma åt detta apiet.(se högre upp)
// eftersom vi kör localhost så kommer det fungerar nu ändå.
app.UseCors("CustomOriginPolicy");

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
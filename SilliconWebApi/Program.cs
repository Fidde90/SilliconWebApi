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

// allowanyheaders inneb�r tex att man kan anv�nda alla olika contenttypes eller keys osv..
// allowanyorigin inneb�r att alla har tillg�ng till apiet.
// allowanymethods inneb�r att man kan g�ra alla metoder som tex post,put,delete,get osv.
// just nu har vi skrivit att bara min adress sak f� komma �t detta apiet.(se h�gre upp)
// eftersom vi k�r localhost s� kommer det fungerar nu �nd�.
app.UseCors("CustomOriginPolicy");

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
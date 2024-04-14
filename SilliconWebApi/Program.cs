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
        .WithOrigins("https://localhost:7198")
        .AllowAnyHeader()
        .AllowAnyMethod();
        //.AllowAnyOrigins();
    });
});

builder.Services.RegisterRepositories(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

// allowanyheaders inneb�r tex att man kan anv�nda alla olika contenttypes eller keys osv..
// allowanyorigin inneb�r att alla har tillg�ng till apiet.
// allowanymethods inneb�r att man kan g�ra alla metoder som tex post,put,delete,get osv.
// just nu har vi skrivit att bara min adress sak f� komma �t detta apiet.(se h�gre upp)
// eftersom vi k�r localhost s� kommer det fungerar nu �nd�.
app.UseCors("CustomOriginPolicy");

app.UseSwagger();
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json","Silicon Web Api v1"));
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
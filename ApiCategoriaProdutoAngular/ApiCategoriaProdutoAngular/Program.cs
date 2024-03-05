using ApiCategoriaProdutoAngular.Context;
using ApiCategoriaProdutoAngular.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<TokenService>();

var myMemoryConnection = builder.Configuration.GetConnectionString("InMemoryData");

if (myMemoryConnection != null )
{
    builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(myMemoryConnection));
}

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.WithOrigins("http://localhost:50779")
    .AllowAnyHeader()
    .AllowAnyMethod()
);

app.UseAuthorization();

app.MapControllers();

app.Run();

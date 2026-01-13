using FnacDarty.TechnicalTest.LibraryManagement.Domain.Repositories;
using FnacDarty.TechnicalTest.LibraryManagement.Domain.Services;
using FnacDarty.TechnicalTest.LibraryManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigureServices(builder.Services);

//Solution

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();


void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<IBookRepository, BookRepository>();
    services.AddSingleton<ICustomerRepository, CustomerRepository>();
    services.AddScoped<ILibraryService, LibraryService>();
}
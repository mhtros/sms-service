using SmsSendingApp.Contracts;
using SmsSendingApp.Data;
using SmsSendingApp.Extensions;
using SmsSendingApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddVendorFactory();

builder.Services.AddSingleton<DatabaseContext>();
builder.Services.AddScoped<ISmsRepository, SmsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
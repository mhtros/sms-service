using SmsSendingApp.Contracts;
using SmsSendingApp.Data;
using SmsSendingApp.Extensions;
using SmsSendingApp.Repositories;
using SmsSendingApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddVendorFactory();
builder.Services.AddModelValidations();

builder.Services.AddSingleton<DatabaseContext>();
builder.Services.AddScoped<ISmsRepository, SmsRepository>();
builder.Services.AddScoped<IVendorResolver, VendorResolver>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
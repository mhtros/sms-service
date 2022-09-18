using System.Diagnostics;
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SmsSendingApp.Contracts;
using SmsSendingApp.Data;
using SmsSendingApp.Entities;
using SmsSendingApp.Exceptions;
using SmsSendingApp.Extensions;
using SmsSendingApp.Models;
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

app.MapPost("sms", async (
        [FromBody] SmsModel model,
        [FromServices] IValidator<SmsModel> validator,
        [FromServices] IVendorResolver vendorResolver) =>
    {
        var validationResult = await validator.ValidateAsync(model);

        if (validationResult.IsValid is false)
        {
            var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(errors);
        }

        var vendor = vendorResolver.ResolveStrategy(model.ReceiverCountryCode);
        var requestId = Activity.Current?.Id ?? Guid.NewGuid().ToString();

        var sms = new Sms
        {
            ReceiverNumber = model.ReceiverNumber,
            SenderEmail = model.SenderEmail,
            ReceiverCountryCode = model.ReceiverCountryCode,
            Message = model.Message,
            RequestId = requestId
        };

        try
        {
            await vendor.SendAsync(sms);
            return Results.Ok(requestId);
        }
        catch (MessageContainsNonGreekCharactersException)
        {
            return Results.BadRequest(new { Message = "Message Contains non Greek Characters" });
        }
    })
    .Produces((int)HttpStatusCode.Created)
    .ProducesProblem((int)HttpStatusCode.BadRequest);

app.Run();
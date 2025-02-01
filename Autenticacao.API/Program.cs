
using CRUDReactJSNetCore.Application;
using CRUDReactJSNetCore.Application.Feature.Auth.Command.AlterarPassword;
using CRUDReactJSNetCore.Application.Feature.Auth.Command.EfetuarLogin;
using CRUDReactJSNetCore.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Autenticacao.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //Add Serilog Config
            builder.Host.UseSerilog((context, configuration) =>
                        configuration.ReadFrom.Configuration(context.Configuration)
                        .MinimumLevel.Debug()
                        .MinimumLevel.Verbose()
                        .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
                        .WriteTo.Console()
                        .WriteTo.File(
                            path: Path.Combine(Directory.GetCurrentDirectory(), "logs", "log-.txt"),
                            rollingInterval: RollingInterval.Day,
                            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}}"
                         )
                        .Filter.ByIncludingOnly(logEvent =>
                        logEvent.MessageTemplate.Text.Contains("LogLevel")));



            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.RegisterApplicationServices();
            builder.Services.RegisterInfrastructureServices(builder.Configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            #region Commands

            app.MapPost("/api/login", async Task<IResult> ([FromBody] EfetuarLoginCommand requestBody, IMediator mediator) =>
            {
                try
                {

                    RequestHelper.ValidarPayload(requestBody);

                    var result = await mediator.Send(requestBody);

                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return RequestHelper.ExceptionResult(ex);
                }
            });

            app.MapPatch("/api/alterar-password", async Task<IResult> ([FromBody] AlterarPasswordCommand requestBody, IMediator mediator) =>
            {
                try
                {

                    RequestHelper.ValidarPayload(requestBody);

                    var result = await mediator.Send(requestBody);

                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return RequestHelper.ExceptionResult(ex);
                }
            });

            #endregion Comands


            app.Run();
        }
    }
}

using CRUDReactJSNetCore.Application;
using CRUDReactJSNetCore.Application.Feature.Funcionario.Command.AlterarFuncionario;
using CRUDReactJSNetCore.Application.Feature.Funcionario.Command.DesativarFuncionario;
using CRUDReactJSNetCore.Application.Feature.Funcionario.Command.ExcluirFuncionario;
using CRUDReactJSNetCore.Application.Feature.Funcionario.Command.InserirFuncionario;
using CRUDReactJSNetCore.Application.Feature.Funcionario.Command.ReativarFuncionario;
using CRUDReactJSNetCore.Application.Feature.Funcionario.Query.GetFuncionarioById;
using CRUDReactJSNetCore.Application.Feature.Funcionario.Query.ListCargos;
using CRUDReactJSNetCore.Application.Feature.Funcionario.Query.ListFuncionario;
using CRUDReactJSNetCore.Application.Feature.Funcionario.Query.ListGestores;
using CRUDReactJSNetCore.Infrastructure;
using CRUDReactJSNetCore.Infrastructure.ContextDb;
using Funcionario.Api;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Text.Json.Serialization;

namespace CRUDReactJSNetCore.API
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

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //Chamada para criação do banco de dados
            using (var scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<CRUDReactJSNetCoreDbContent>();
            }





            #region Commands

            app.MapPost("/api", async Task<IResult> (InserirFuncionarioRequest requestBody, IMediator mediator) =>
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

            app.MapPut("/api/{id:long}", async Task<IResult> (long id, AlterarFuncionarioRequest requestBody, IMediator mediator) =>
            {
                try
                {
                    requestBody.Id = id;
                    RequestHelper.ValidarPayload(requestBody);

                    var result = await mediator.Send(requestBody);

                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return RequestHelper.ExceptionResult(ex);
                }
            });

            app.MapPatch("/api/{id:long}/desativar", async Task<IResult> (long id, IMediator mediator) =>
            {
                try
                {
                    var result = await mediator.Send(new DesativarFuncionarioRequest { FuncionarioId = id });

                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return RequestHelper.ExceptionResult(ex);
                }
            });

            app.MapPatch("/api/{id:long}/reativar", async Task<IResult> (long id, IMediator mediator) =>
            {
                try
                {
                    var result = await mediator.Send(new ReativarFuncionarioRequest { FuncionarioId = id });

                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return RequestHelper.ExceptionResult(ex);
                }
            });

            app.MapDelete("/api/{id:long}", async Task<IResult> (long id, IMediator mediator) =>
            {
                try
                {
                    var result = await mediator.Send(new ExcluirFuncionarioRequest { FuncionarioId = id });

                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return RequestHelper.ExceptionResult(ex);
                }
            });


            #endregion Commands


            #region Queries

            app.MapGet("/api/cargos", async Task<IResult> (IMediator mediator) =>
            {
                try
                {

                    var result = await mediator.Send(new ListCargosRequest());

                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return RequestHelper.ExceptionResult(ex);
                }
            });

            app.MapGet("/api/gestores/{level}", async Task<IResult> (int level, IMediator mediator) =>
            {
                try
                {
                    var result = await mediator.Send(new ListGestoresRequest() { LevelCargo = level });

                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return RequestHelper.ExceptionResult(ex);
                }
            });


            app.MapGet("/api/{pageIndex}/{pageCount}", async Task<IResult> ([FromRoute] int pageIndex, [FromRoute] int pageCount, [FromQuery] string? filter, IMediator mediator) =>
            {
                try
                {
                    var result = await mediator.Send(new ListFuncionarioRequest() { PageIndex = pageIndex, PageCount = pageCount, Filtro = filter });

                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return RequestHelper.ExceptionResult(ex);
                }
            });

            app.MapGet("/api", async Task<IResult> ([FromQuery] long id, IMediator mediator) =>
            {
                try
                {
                    var result = await mediator.Send(new GetFuncionarioByIdRequest() { FuncionarioId = id });

                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return RequestHelper.ExceptionResult(ex);
                }
            });

            #endregion Queries




            app.Run();
        }
    }
}
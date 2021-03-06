﻿using AivenEcommerce.V1.Domain.Shared.OperationResults;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using System;
using System.Net;
using System.Text;
using System.Text.Json;

namespace AivenEcommerce.V1.WebApi.Startup
{
    public static class ExceptionHandlingExceptions
    {
        public static IApplicationBuilder UseExceptionHandlingOperationResult(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature is not null)
                    {
                        logger.LogError(contextFeature.Error, FlattenException(contextFeature.Error));

                        ValidationResult validationResult = new();
                        validationResult.Messages.Add(new("Internal Server Error: " + contextFeature.Error.Message));

                        await context.Response.WriteAsync(JsonSerializer.Serialize(OperationResult.Error(validationResult)));
                    }

                    static string FlattenException(Exception exception)
                    {
                        StringBuilder stringBuilder = new();

                        while (exception is not null)
                        {
                            stringBuilder.AppendLine(exception.Message);
                            stringBuilder.AppendLine(exception.StackTrace);

                            exception = exception.InnerException;
                        }

                        return stringBuilder.ToString();
                    }
                });
            });
            return app;
        }
    }
}

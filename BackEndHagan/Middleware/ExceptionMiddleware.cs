﻿
using BackEndHagan.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BackEndHagan.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware ( RequestDelegate next,
        IHostEnvironment env,
        ILogger<ExceptionMiddleware> logger )
        {
            _logger = logger;
            _next = next;
            _env = env;
        }


        public async Task InvokeAsync ( HttpContext context )
        {
            try
            {
                await _next (context);
            }
            catch ( Exception ex )
            {
                _logger.LogError (ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ( int ) HttpStatusCode.InternalServerError;
                var response=_env.IsDevelopment()
                  ?new ApiException(context.Response.StatusCode,ex.Message,ex.StackTrace?.ToString()):
                   new ApiException(context.Response.StatusCode,ex.Message, "Internal Server Error");
                var options=new JsonSerializerOptions{PropertyNamingPolicy=JsonNamingPolicy.CamelCase};

                var json=JsonSerializer.Serialize(response,options);

                await context.Response.WriteAsync (json);

            }
        }
    }
}

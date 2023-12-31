﻿using System.Net;
using UrlShortener.Common.Models.Responses;

namespace UrlShortener.API.Extensions {
    public class ExceptionMiddleware {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger) {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext) {
            try {
                await _next(httpContext);
            }
            catch (Exception ex) {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception) {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new ErrorResponse() {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error"
            }.ToString());
        }

    }
}

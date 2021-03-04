using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Points.Dtos;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Points.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var result = JsonConvert.SerializeObject(new ErrorDto
                {
                    Error = exception?.Message
                });
                await response.WriteAsync(result);
            }
        }
    }
}

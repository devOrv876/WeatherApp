//https://alexbierhaus.medium.com/api-request-and-response-logging-middleware-using-net-5-c-a0af639920da

using Azure.Core;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using WeatherApp.Api.Exceptions;

namespace WeatherApp.Api.Middleware
{
    public class ApiTrafficMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiTrafficMiddleware> _logger;

        public ApiTrafficMiddleware(RequestDelegate next, ILogger<ApiTrafficMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBody = context.Response.Body;

            //First, get the incoming request
            var request = await GetRequestString(context.Request);

            _logger.LogInformation(request);

            //create a new memory stream and use it for the temp response body
            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            //continue down the middleware pipeline
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                await HandleExceptionResponseAsync(context, ex);
            }

            //format the response from the server
            var response = await GetResponseAsTextAsync(context.Response);

            //log response
            _logger.LogInformation(response);

            //copy the contents of the new memory stream which contains the response to the original stream, which is then returned to the client.
            await responseBody.CopyToAsync(originalBody);
            
        }

        private async Task<string> GetResponseAsTextAsync(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }


        private async Task<string> GetRequestString(HttpRequest request)
        {
            //var body = request.Body;

            //set the reader for the request back at the begining of its stream
            request.EnableBuffering();
         
            string bodyContent = await new StreamReader(request.Body).ReadToEndAsync();
            //assign the read body back to the request body
            request.Body.Position = 0;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyContent}";
        }

        private Task HandleExceptionResponseAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode status;
            string message;

            var exceptionType = ex.GetType();

            if (exceptionType == typeof(InvalidRequestException))
            {
                message = ex.Message;
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                message = ex.Message;
                status = HttpStatusCode.NotFound;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                status = HttpStatusCode.NotImplemented;
                message = ex.Message;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
                message = ex.Message;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            var exceptionResult = JsonConvert.SerializeObject(new ErrorDetails(context.Response.StatusCode, message));
            return context.Response.WriteAsync(exceptionResult);
        }
    }
}

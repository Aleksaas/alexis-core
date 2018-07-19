using AlexisCorePro.Business.Common.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AlexisCorePro.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        ILogger<GlobalExceptionFilter> logger = null;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> exceptionLogger)
        {
            logger = exceptionLogger;
        }

        public void OnException(ExceptionContext context)
        {
            string content = "";

            var result = new ContentResult();

            if (context.Exception is ValidationError)
            {
                var exception = context.Exception as ValidationError;

                content = JsonConvert
                    .SerializeObject(new Response<object>(null, exception.Errors), new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });

                context.HttpContext.Response.StatusCode = 400;
            }
            else
            {
                // TO DO: check if (env.IsDevelopment())
                logger.LogError(0, context.Exception.GetBaseException(), "Exception occurred. " + context.Exception.Message);

                content = JsonConvert
                    .SerializeObject(new Response<object>(null, new ApiError(500, "Something went wrong. " + context.Exception.Message)), new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });

                context.HttpContext.Response.StatusCode = 500;
            }

            result.Content = content;
            result.ContentType = "application/json";

            context.Result = result;
        }
    }
}

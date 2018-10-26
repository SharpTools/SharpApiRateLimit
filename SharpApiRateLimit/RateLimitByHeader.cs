using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpApiRateLimit.Storage;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SharpApiRateLimit {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RateLimitByHeader : Attribute, IAsyncResourceFilter {
        private readonly string _headerName;
        private readonly string _period;
        private readonly int _maxCalls;

        public RateLimitByHeader(string headerName, string period, int calls = 1) {
            _headerName = headerName;
            _period = period;
            _maxCalls = calls;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next) {
            var info = context.ActionDescriptor as ControllerActionDescriptor;

            if(context.Filters.Last(f => f is RateLimitByHeader) != this) {
                await next();
                return;
            }
            var headers = context.HttpContext.Request.Headers;
            var id = headers.Keys.Contains(_headerName) ? headers[_headerName].FirstOrDefault() : "";
            var key = $"{id}-{info.AttributeRouteInfo.Template}";

            var storage = context.GetService<IRateLimitStorage>();
            
            var result = storage.Process(key, _period, _maxCalls);
            if (result.IsSuccess) {
                SetHeaders(context.HttpContext, result);
                await next();
                return;
            }
            await ReponseWithTooManyRequests(context, result);
            return;
        }

        private static void SetHeaders(HttpContext context, RateLimitResult result) {
            var headers = context.Response.Headers;
            headers.Add("X-RateLimit-Limit", result.RequestLimit.ToString());
            headers.Add("X-RateLimit-Remaining", result.RequestsRemaining.ToString());
            headers.Add("X-RateLimit-Reset", result.GetRateLimitResetAsUnixtime().ToString());
        }

        private async Task ReponseWithTooManyRequests(ResourceExecutingContext context, RateLimitResult result) {
            var serializer = context.GetService<IRateLimitResponseSerializer>();
            var json = serializer.ToJson(result);
            var response = context.HttpContext.Response;

            response.Headers["Retry-After"] = result.GetRateLimitResetAsUnixtime().ToString();
            response.StatusCode = StatusCodes.Status429TooManyRequests;
            response.ContentType = "application/json";
            await response.WriteAsync(json);
        }
    }
}
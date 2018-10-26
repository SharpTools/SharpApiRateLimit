using Microsoft.AspNetCore.Mvc.Filters;

namespace SharpApiRateLimit {
    public static class ResourceExecutingContextExt {
        public static T GetService<T>(this ResourceExecutingContext context) where T : class {
            return context.HttpContext.RequestServices.GetService(typeof(T)) as T;
        }
    }
}
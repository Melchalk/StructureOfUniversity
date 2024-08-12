using Microsoft.AspNetCore.Rewrite;
using StructureOfUniversity.DTOs.Enums;

namespace StructureOfUniversity.Infrastructure;

public static class RewriterOptions
{
    private const string UserPosition = "UserPosition";

    public static void RewriteRequests(RewriteContext context)
    {
        var httpContext = context.HttpContext;

        var position = httpContext.Items[UserPosition];
        var path = httpContext.Request.Path.Value;

        if (position is null || path is null)
        {
            return;
        }

        if (position.ToString() == TeachingPositions.Assistant.ToString() &&
            path.Contains("faculty/update"))
        {
            string proccedPath = path.Replace("update", "get/all");

            httpContext.Response.ContentType = "application/json";

            httpContext.Request.Path = new PathString(proccedPath);
            httpContext.Request.Method = "GET";

            httpContext.Request.RouteValues["action"] = "GetFaculties";
        }

    }
}

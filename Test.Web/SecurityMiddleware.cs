using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Test.Db.RLS;
using Test.Db.Model.RLS;

namespace Test.Web
{
    public class SecurityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        private object lockObj = new object();

        public SecurityMiddleware(RequestDelegate next, ILogger<SecurityMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-2.0&tabs=aspnetcore2x#per-request-dependencies
        public async Task Invoke(HttpContext context, CurrentUserProvider currentUser,
            PermissionService permissionService)
        {
            var userName = context.User.Identity.Name;
            _logger.LogInformation($"Invoke - {userName}");

            currentUser.SetIdentifier(userName);
            SecurityIdentity identity;
            lock (lockObj)
            {
                identity = permissionService.RegisterUserIfNotExists(userName ?? "Anonymous");
            }
            currentUser.SetUser(identity);

            await _next(context);
        }
    }
}

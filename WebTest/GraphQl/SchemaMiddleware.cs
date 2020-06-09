using GraphQL.Types;
using GraphQL.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebTest.GraphQl
{
    public class SchemaMiddleware
    {
        private readonly RequestDelegate _next;

        public SchemaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ISchema schema)
        {
            using (var printer = new SchemaPrinter(schema))
            {
                context.Response.ContentType = "application/text";
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                await context.Response.WriteAsync(printer.Print());

                return;
            }
        }
    }
}

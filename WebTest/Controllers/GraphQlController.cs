using GraphQL;
using GraphQL.Instrumentation;
using GraphQL.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTest.GraphQl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Controllers
{
    [Authorize]
    [Route("api/graphql")]
    public class GraphQlController : Controller
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;

        public GraphQlController(ISchema schema, IDocumentExecuter documentExecuter)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLRequest request)
        {
            if (request == null) { throw new ArgumentNullException(nameof(request)); }
            //var files = Request.Form.Files;
            var settings = new GraphQLSettings
            {
                Path = "/api/graphql",
                BuildUserContext = ctx => new GraphQLUserContext
                {
                    User = ctx.User,
                    //Files = request.File
                },
                EnableMetrics = false
            };
            var result = await _documentExecuter.ExecuteAsync(_ =>
            {
                _.Schema = _schema;
                _.Query = request?.Query;
                _.OperationName = request?.OperationName;
                _.Inputs = request?.Variables.ToInputs();
                _.UserContext = settings.BuildUserContext?.Invoke(this.HttpContext);
                //_.ValidationRules = DocumentValidator.CoreRules.Concat(new[] { new InputValidationRule() });
                _.EnableMetrics = settings.EnableMetrics;
                if (settings.EnableMetrics)
                {
                    _.FieldMiddleware.Use<InstrumentFieldsMiddleware>();
                }
            });

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}

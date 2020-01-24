using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Web.Controllers
{
    public class DeleteController : Controller
    {
        public PermissionService PermissionService { get; }

        public DeleteController(PermissionService permissionService)
        {
            PermissionService = permissionService;
        }

        public IActionResult PutMeToAdminGroup()
        {
            PermissionService.AddCurrentUserToAdminGroup();

            return Ok();
        }
    }
}

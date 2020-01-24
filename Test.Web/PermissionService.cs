using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Db.Model.RLS;
using Test.Db.Repositories.RLS;
using Test.Db.RLS;
using Test.Db.Utils;

namespace Test.Web
{
    public class PermissionService
    {
        private readonly ISecurityObjectRepository secObjectRepository;
        private CurrentUserProvider CurrentUser;
        private readonly ILogger _logger;
        public PermissionService(ISecurityObjectRepository SecObjectRepository, CurrentUserProvider CurrentUser,
            ILogger<PermissionService> _logger)
        {
            this.secObjectRepository = SecObjectRepository;
            this.CurrentUser = CurrentUser;
            this._logger = _logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifier"></param>
        public SecurityIdentity RegisterUserIfNotExists(string identifier)
        {
            var identity = secObjectRepository.GetAll().OfType<SecurityIdentity>().Where(x => x.Identifier.Equals(identifier)).SingleOrDefault();

            if (identity == null)
            {
                var newUser = new SecurityIdentity
                {
                    Identifier = identifier,
                    Email = "some@address.com",
                    FullName = $"Jhon Doe / {identifier}"
                };
                secObjectRepository.Add(newUser);
                secObjectRepository.SaveChanges();

                return newUser;
            }
            else
            {
                return identity;
            }
        }

        public void AddCurrentUserToAdminGroup()
        {
            var adminGroup = secObjectRepository.GetById(2L) as SecurityGroup;
            var user = secObjectRepository.GetIdentity(CurrentUser.Identity.Id);
            if (!user.GroupMemberShips.Where(x => x.SecurityGroupId == 2).Any())
            {
                adminGroup.GroupMembers.Add(new SecurityGroupSecurityIdentity
                {
                    SecurityGroupId = adminGroup.Id,
                    SecurityIdentityId = user.Id
                });
                secObjectRepository.SaveChanges();
            }
        }
    }
}

using DbTest.Model.RLS;
using DbTest.Repositories.RLS;
using DbTest.RLS;
using DbTest.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest
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
        /// Ad-ba kérés sokáig tarthat. Azért van először külön beszúrás majd AD-ba hívás után entitás frissítés.
        /// </summary>
        /// <param name="identifier"></param>
        public SecurityIdentity RegisterUserIfNotExists(string identifier)
        {
            var identity = secObjectRepository.GetAll()
                .OfType<SecurityIdentity>()
                .Include(x => x.GroupMemberShips)
                .ThenInclude(x => x.Group)
                .Where(x => x.Identifier.Equals(identifier)).SingleOrDefault();

            if (identity == null)
            {
                var newUser = new SecurityIdentity
                {
                    Identifier = identifier
                };
                secObjectRepository.Add(newUser);
                secObjectRepository.SaveChanges();

                using (var adHandler = new UserRegisterFromAd())
                {
                    var result = adHandler.GetADUser(identifier.Replace("MVMH\\", ""));

                    newUser.Email = result.EmailAddress;
                    newUser.FullName = result.Name;

                    secObjectRepository.Update(newUser);
                    secObjectRepository.SaveChanges();

                    return newUser;
                }
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

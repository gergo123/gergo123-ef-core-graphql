using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using DbTest.Model.Placeholder;
using DbTest.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using DbTest;
using DbTest.Utils;
using DbTest.RLS;
using EFTest.Rules;
using DbTest.Model.Configuration;
using System.ComponentModel.DataAnnotations;
using DbTest.Model.RLS;
using DbTest.Stepper.Model.RLS;
using DbTest.Stepper;
using DbTest.Stepper.Model.Workflow;

namespace DbTest.Model
{
    public class CoreContext : DbContext
    {
        public DbSet<PlaceholderEntity> Placeholder { get; set; }
        public DbSet<PlaceholderEntityACL> PlaceholderACL { get; set; }
        public DbSet<SecurityObject> SecurityObjects { get; set; }
        public DbSet<RLS.SecurityGroup> SecurityGroups { get; set; }
        public DbSet<RLS.SecurityIdentity> SecurityIdentity { get; set; }
        public DbSet<SimplePlaceHolderEntity> SimplePlaceHolders { get; set; }
        public DbSet<BasicTask> BasicTasks { get; set; }
        public DbSet<BasicTaskAcl> BasicTaskAcl { get; set; }
        public DbSet<TestEntityModel> TestEntity { get; set; }
        public DbSet<SecurityGroupSecurityIdentity> SecurityGroupSecurityIdentities { get; set; }

        private readonly CurrentUserProvider UserProvider;

        public CoreContext(CurrentUserProvider userProvider, DbContextOptions<CoreContext> options)
            : base(options)
        {
            UserProvider = userProvider;
        }

        private readonly EFTest.Rules.RuleManager ruleEngine = RuleFactory.GetRuleManager();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlaceholderEntityConfig());

            modelBuilder.Entity<SecurityGroupSecurityIdentity>()
                .HasKey(bc => new { bc.SecurityGroupId, bc.SecurityIdentityId });
            modelBuilder.Entity<SecurityGroupSecurityIdentity>()
                .HasOne(bc => bc.Identity)
                .WithMany(b => b.GroupMemberShips)
                .HasForeignKey(bc => bc.SecurityIdentityId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SecurityGroupSecurityIdentity>()
                .HasOne(bc => bc.Group)
                .WithMany(c => c.GroupMembers)
                .HasForeignKey(bc => bc.SecurityGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SecurityGroup>().HasData(new SecurityGroup { Id = 2, Name = "Administrators" });

            base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //Configuration.ProxyCreationEnabled = true;
        }

        public override int SaveChanges()
        {
            AddTimestamps();

            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                               || e.State == EntityState.Modified
                               || e.State == EntityState.Deleted
                           select e;
            var validationResults = new List<ValidationResult>();
            // 2x fut vajon le?
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.TryValidateObject(
                    entity,
                    validationContext,
                    validationResults,
                    validateAllProperties: true);

                validationResults.AddRange(ruleEngine.ExecuteRules(entity));
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private string currentUsername => SeedUserProvider.User ?? UserProvider.CurrentUserIdentifier;

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is IChangeTrackingBase && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((IChangeTrackingBase)entity.Entity).DateCreated = DateTime.Now;
                    ((IChangeTrackingBase)entity.Entity).UserCreated = currentUsername;
                    ((IChangeTrackingBase)entity.Entity).CreatedSecurityId = UserProvider.Identity.Id;
                }

                if (entity.State == EntityState.Modified)
                {
                    ((IChangeTrackingBase)entity.Entity).DateModified = DateTime.Now;
                    ((IChangeTrackingBase)entity.Entity).UserModified = currentUsername;
                    ((IChangeTrackingBase)entity.Entity).UpdatedSecurityId = UserProvider.Identity.Id;
                }
            }
        }
    }
}

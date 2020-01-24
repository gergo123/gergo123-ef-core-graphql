using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using Test.Db.Model.Placeholder;
using Test.Db.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using Test.Db;
using Test.Db.Utils;
using Test.Db.RLS;
using Test.Db.Model.Configuration;
using Test.Db.Stepper.Model.Workflow;
using Test.Db.Model.RLS;

namespace Test.Db.Model
{
    public class CoreContext : DbContext
    {
        public DbSet<PlaceholderEntity> Placeholder { get; set; }
        public DbSet<PlaceholderEntityACL> PlaceholderACL { get; set; }

        public DbSet<RLS.SecurityObject> SecurityObjects { get; set; }
        public DbSet<RLS.SecurityGroup> SecurityGroups { get; set; }
        public DbSet<RLS.SecurityIdentity> SecurityIdentity { get; set; }
        public DbSet<RLS.SecurityGroupSecurityIdentity> SecurityGroupSecurityIdentities { get; set; }

        public DbSet<SimplePlaceHolderEntity> SimplePlaceHolders { get; set; }
        public DbSet<BasicTask> BasicTasks { get; set; }
        public DbSet<BasicTaskAcl> BasicTaskAcl { get; set; }
        public DbSet<TestEntityModel> TestEntity { get; set; }

        private readonly CurrentUserProvider UserProvider;

        public CoreContext(CurrentUserProvider userProvider, DbContextOptions<CoreContext> options)
            : base(options)
        {
            UserProvider = userProvider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlaceholderEntityConfig());
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<SecurityGroup>().HasData(DefaultData.AdminGroup);
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //Configuration.ProxyCreationEnabled = true;
        }

        public override int SaveChanges()
        {
            AddTimestamps();
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
                }

                if (entity.State == EntityState.Modified)
                {
                    ((IChangeTrackingBase)entity.Entity).DateModified = DateTime.Now;
                    ((IChangeTrackingBase)entity.Entity).UserModified = currentUsername;
                }
            }
        }
    }
}

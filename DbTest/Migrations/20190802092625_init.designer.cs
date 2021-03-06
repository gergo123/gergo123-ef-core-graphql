﻿// <auto-generated />
using DbTest.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DbTest.Migrations
{
    [DbContext(typeof(CoreContext))]
    [Migration("20190802092625_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DbTest.Model.Placeholder.PlaceholderEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<string>("LocalString");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Placeholder");
                });

            modelBuilder.Entity("DbTest.Model.Placeholder.PlaceholderEntityACL", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("EntityID");

                    b.Property<int>("Permission");

                    b.Property<long>("SecurityObjectID");

                    b.HasKey("ID");

                    b.ToTable("PlaceholderACL");
                });

            modelBuilder.Entity("DbTest.Model.Placeholder.SimplePlaceHolderEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive");

                    b.Property<string>("SimpleProperty");

                    b.HasKey("Id");

                    b.ToTable("SimplePlaceHolders");
                });

            modelBuilder.Entity("DbTest.Model.RLS.SecurityGroupSecurityIdentity", b =>
                {
                    b.Property<long>("SecurityGroupId");

                    b.Property<long>("SecurityIdentityId");

                    b.HasKey("SecurityGroupId", "SecurityIdentityId");

                    b.HasIndex("SecurityIdentityId");

                    b.ToTable("SecurityGroupSecurityIdentity");
                });

            modelBuilder.Entity("DbTest.Model.RLS.SecurityObject", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("SecurityObjects");

                    b.HasDiscriminator<string>("Discriminator").HasValue("SecurityObject");
                });

            modelBuilder.Entity("DbTest.Model.RLS.SecurityGroup", b =>
                {
                    b.HasBaseType("DbTest.Model.RLS.SecurityObject");

                    b.Property<string>("Name");

                    b.HasDiscriminator().HasValue("SecurityGroup");
                });

            modelBuilder.Entity("DbTest.Model.RLS.SecurityIdentity", b =>
                {
                    b.HasBaseType("DbTest.Model.RLS.SecurityObject");

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<string>("Identifier");

                    b.HasDiscriminator().HasValue("SecurityIdentity");
                });

            modelBuilder.Entity("DbTest.Model.RLS.SecurityGroupSecurityIdentity", b =>
                {
                    b.HasOne("DbTest.Model.RLS.SecurityGroup", "Group")
                        .WithMany("GroupMembers")
                        .HasForeignKey("SecurityGroupId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DbTest.Model.RLS.SecurityIdentity", "Identity")
                        .WithMany("GroupMemberShips")
                        .HasForeignKey("SecurityIdentityId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}

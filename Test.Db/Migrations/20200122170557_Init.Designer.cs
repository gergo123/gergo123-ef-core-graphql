﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Test.Db.Model;

namespace Test.Db.Migrations
{
    [DbContext(typeof(CoreContext))]
    [Migration("20200122170557_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Test.Db.Model.Placeholder.PlaceholderEntity", b =>
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

            modelBuilder.Entity("Test.Db.Model.Placeholder.PlaceholderEntityACL", b =>
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

            modelBuilder.Entity("Test.Db.Model.Placeholder.SimplePlaceHolderEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive");

                    b.Property<string>("SimpleProperty");

                    b.HasKey("Id");

                    b.ToTable("SimplePlaceHolders");
                });

            modelBuilder.Entity("Test.Db.Model.RLS.SecurityGroupSecurityIdentity", b =>
                {
                    b.Property<long>("SecurityGroupId");

                    b.Property<long>("SecurityIdentityId");

                    b.HasKey("SecurityGroupId", "SecurityIdentityId");

                    b.HasIndex("SecurityIdentityId");

                    b.ToTable("SecurityGroupSecurityIdentities");
                });

            modelBuilder.Entity("Test.Db.Model.RLS.SecurityObject", b =>
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

            modelBuilder.Entity("Test.Db.Stepper.Model.Workflow.BasicTask", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ApprovedAt");

                    b.Property<long?>("ApprovedBySecurityIdentityID");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<long>("EntityId");

                    b.Property<string>("Message");

                    b.Property<long?>("SecurityGroupId");

                    b.Property<int>("Status");

                    b.Property<string>("UniqueID");

                    b.Property<string>("UserCreated");

                    b.Property<string>("UserModified");

                    b.HasKey("Id");

                    b.HasIndex("ApprovedBySecurityIdentityID");

                    b.HasIndex("SecurityGroupId");

                    b.ToTable("BasicTasks");
                });

            modelBuilder.Entity("Test.Db.Stepper.Model.Workflow.BasicTaskAcl", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("EntityID");

                    b.Property<int>("Permission");

                    b.Property<long>("SecurityObjectID");

                    b.HasKey("ID");

                    b.ToTable("BasicTaskAcl");
                });

            modelBuilder.Entity("Test.Db.Stepper.Model.Workflow.TestEntityModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CurrentState");

                    b.Property<string>("Message");

                    b.HasKey("Id");

                    b.ToTable("TestEntity");
                });

            modelBuilder.Entity("Test.Db.Model.RLS.SecurityGroup", b =>
                {
                    b.HasBaseType("Test.Db.Model.RLS.SecurityObject");

                    b.Property<string>("Name");

                    b.HasDiscriminator().HasValue("SecurityGroup");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Administrators"
                        });
                });

            modelBuilder.Entity("Test.Db.Model.RLS.SecurityIdentity", b =>
                {
                    b.HasBaseType("Test.Db.Model.RLS.SecurityObject");

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<string>("Identifier");

                    b.HasDiscriminator().HasValue("SecurityIdentity");
                });

            modelBuilder.Entity("Test.Db.Model.RLS.SecurityGroupSecurityIdentity", b =>
                {
                    b.HasOne("Test.Db.Model.RLS.SecurityGroup", "Group")
                        .WithMany("GroupMembers")
                        .HasForeignKey("SecurityGroupId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Test.Db.Model.RLS.SecurityIdentity", "Identity")
                        .WithMany("GroupMemberShips")
                        .HasForeignKey("SecurityIdentityId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Test.Db.Stepper.Model.Workflow.BasicTask", b =>
                {
                    b.HasOne("Test.Db.Model.RLS.SecurityIdentity", "ApprovedBySecurityIdentity")
                        .WithMany("AssignedTasks")
                        .HasForeignKey("ApprovedBySecurityIdentityID");

                    b.HasOne("Test.Db.Model.RLS.SecurityGroup")
                        .WithMany("AssignedTasks")
                        .HasForeignKey("SecurityGroupId");
                });
#pragma warning restore 612, 618
        }
    }
}

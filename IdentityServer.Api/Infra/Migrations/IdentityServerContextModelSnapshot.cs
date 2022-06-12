﻿// <auto-generated />
using System;
using IdentityServer.Api.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IdentityServer.Api.Infra.Migrations
{
    [DbContext(typeof(IdentityServerContext))]
    partial class IdentityServerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("IdentityServer.Api.Domain.Models.Claim", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<Guid>("Code")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Claim", (string)null);
                });

            modelBuilder.Entity("IdentityServer.Api.Domain.Models.Client", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ClientSecret")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("Code")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Client", (string)null);
                });

            modelBuilder.Entity("IdentityServer.Api.Domain.Models.ClientClaim", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("ClaimId")
                        .HasColumnType("bigint");

                    b.Property<long>("ClientId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("Code")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ClaimId");

                    b.HasIndex("ClientId");

                    b.ToTable("ClientClaim", (string)null);
                });

            modelBuilder.Entity("IdentityServer.Api.Domain.Models.ClientScope", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("ClientId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("Code")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("ScopeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ScopeId");

                    b.ToTable("ClientScope", (string)null);
                });

            modelBuilder.Entity("IdentityServer.Api.Domain.Models.LoginHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<Guid>("Code")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("LoginHistory", (string)null);
                });

            modelBuilder.Entity("IdentityServer.Api.Domain.Models.Resource", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<Guid>("Code")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Resource", (string)null);
                });

            modelBuilder.Entity("IdentityServer.Api.Domain.Models.Scope", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<Guid>("Code")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Scope", (string)null);
                });

            modelBuilder.Entity("IdentityServer.Api.Domain.Models.ScopeResource", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<Guid>("Code")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("ResourceId")
                        .HasColumnType("bigint");

                    b.Property<long>("ScopeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ResourceId");

                    b.HasIndex("ScopeId");

                    b.ToTable("ScopeResource");
                });

            modelBuilder.Entity("IdentityServer.Api.Domain.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("Code")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Identification")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("IdentityServer.Api.Domain.Models.UserClaim", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("ClaimId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("Code")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ClaimId");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim", (string)null);
                });

            modelBuilder.Entity("IdentityServer.Api.Domain.Models.ClientClaim", b =>
                {
                    b.HasOne("IdentityServer.Api.Domain.Models.Claim", "Claim")
                        .WithMany("Clients")
                        .HasForeignKey("ClaimId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("IdentityServer.Api.Domain.Models.Client", "Client")
                        .WithMany("Claims")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Claim");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("IdentityServer.Api.Domain.Models.ClientScope", b =>
                {
                    b.HasOne("IdentityServer.Api.Domain.Models.Client", "Client")
                        .WithMany("Scopes")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("IdentityServer.Api.Domain.Models.Scope", "Scope")
                        .WithMany("Clients")
                        .HasForeignKey("ScopeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Scope");
                });

            modelBuilder.Entity("IdentityServer.Api.Domain.Models.LoginHistory", b =>
                {
                    b.HasOne("IdentityServer.Api.Domain.Models.User", "User")
                        .WithMany("History")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("IdentityServer.Api.Domain.Models.ScopeResource", b =>
                {
                    b.HasOne("IdentityServer.Api.Domain.Models.Resource", "Resource")
                        .WithMany("Scopes")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("IdentityServer.Api.Domain.Models.Scope", "Scope")
                        .WithMany("Resources")
                        .HasForeignKey("ScopeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Resource");

                    b.Navigation("Scope");
                });

            modelBuilder.Entity("IdentityServer.Api.Domain.Models.UserClaim", b =>
                {
                    b.HasOne("IdentityServer.Api.Domain.Models.Claim", "Claim")
                        .WithMany("Users")
                        .HasForeignKey("ClaimId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("IdentityServer.Api.Domain.Models.User", "User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Claim");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IdentityServer.Api.Domain.Models.Claim", b =>
                {
                    b.Navigation("Clients");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("IdentityServer.Api.Domain.Models.Client", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("Scopes");
                });

            modelBuilder.Entity("IdentityServer.Api.Domain.Models.Resource", b =>
                {
                    b.Navigation("Scopes");
                });

            modelBuilder.Entity("IdentityServer.Api.Domain.Models.Scope", b =>
                {
                    b.Navigation("Clients");

                    b.Navigation("Resources");
                });

            modelBuilder.Entity("IdentityServer.Api.Domain.Models.User", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("History");
                });
#pragma warning restore 612, 618
        }
    }
}
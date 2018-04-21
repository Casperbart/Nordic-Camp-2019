﻿// <auto-generated />
using Backend.Repository.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Backend.Repository.EF.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20180420194752_UserInitial")]
    partial class UserInitial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Backend.Model.Page", b =>
                {
                    b.Property<string>("Url")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.HasKey("Url");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("Backend.Model.Users.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Price");

                    b.HasKey("Id");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("Backend.Model.Users.ActivityRegistration", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("ActivityId");

                    b.HasKey("UserId", "ActivityId");

                    b.HasIndex("ActivityId");

                    b.ToTable("ActivityRegistrations");
                });

            modelBuilder.Entity("Backend.Model.Users.Guardian", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Guardians");
                });

            modelBuilder.Entity("Backend.Model.Users.User", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("Allergies");

                    b.Property<bool>("Alumni");

                    b.Property<DateTime>("Birthday");

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhotoPermission");

                    b.Property<string>("Remarks");

                    b.Property<string>("SubjectId");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Backend.Model.Users.ActivityRegistration", b =>
                {
                    b.HasOne("Backend.Model.Users.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Backend.Model.Users.User", "User")
                        .WithMany("ActivityRegistrations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Backend.Model.Users.Guardian", b =>
                {
                    b.HasOne("Backend.Model.Users.User")
                        .WithMany("Guardians")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}

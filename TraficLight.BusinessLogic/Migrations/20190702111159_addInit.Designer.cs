﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TraficLight.BusinessLogic.Entities;

namespace TraficLight.BusinessLogic.Migrations
{
    [DbContext(typeof(TraficContext))]
    [Migration("20190702111159_addInit")]
    partial class addInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("TraficLight.BusinessLogic.Entities.Observation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color");

                    b.Property<string>("NumberOne");

                    b.Property<string>("NumberTwo");

                    b.Property<Guid>("SequenceId");

                    b.HasKey("Id");

                    b.HasIndex("SequenceId");

                    b.ToTable("Observations");
                });

            modelBuilder.Entity("TraficLight.BusinessLogic.Entities.Sequence", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Sequences");
                });

            modelBuilder.Entity("TraficLight.BusinessLogic.Entities.Observation", b =>
                {
                    b.HasOne("TraficLight.BusinessLogic.Entities.Sequence", "Sequence")
                        .WithMany("Observations")
                        .HasForeignKey("SequenceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelApi.Models;

#nullable disable

namespace TravelApi.Migrations
{
    [DbContext(typeof(TravelApiContext))]
    partial class TravelApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TravelApi.Models.Destination", b =>
                {
                    b.Property<int>("DestinationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<int>("ReviewCount")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("int");

                    b.HasKey("DestinationId");

                    b.ToTable("Destinations");
                });

            modelBuilder.Entity("TravelApi.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("DestinationId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)");

                    b.HasKey("ReviewId");

                    b.HasIndex("DestinationId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("TravelApi.Models.Review", b =>
                {
                    b.HasOne("TravelApi.Models.Destination", null)
                        .WithMany("Reviews")
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TravelApi.Models.Destination", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}

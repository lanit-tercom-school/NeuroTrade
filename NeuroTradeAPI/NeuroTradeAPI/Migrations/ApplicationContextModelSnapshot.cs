﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using NeuroTradeAPI;
using System;

namespace NeuroTradeAPI.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("NeuroTradeAPI.Batch", b =>
                {
                    b.Property<int>("BatchId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BeginTime");

                    b.Property<DateTime?>("EndTime");

                    b.Property<int>("InstrumentId");

                    b.Property<TimeSpan>("Interval");

                    b.HasKey("BatchId");

                    b.HasIndex("InstrumentId");

                    b.ToTable("Batches");
                });

            modelBuilder.Entity("NeuroTradeAPI.Candle", b =>
                {
                    b.Property<long>("CandleId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BatchId");

                    b.Property<DateTime?>("BeginTime");

                    b.Property<float>("Close");

                    b.Property<float>("High");

                    b.Property<float>("Low");

                    b.Property<float>("Open");

                    b.Property<int>("Volume");

                    b.HasKey("CandleId");

                    b.HasIndex("BatchId");

                    b.ToTable("Candles");
                });

            modelBuilder.Entity("NeuroTradeAPI.Instrument", b =>
                {
                    b.Property<int>("InstrumentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias");

                    b.Property<string>("DownloadAlias");

                    b.HasKey("InstrumentId");

                    b.ToTable("Instruments");
                });

            modelBuilder.Entity("NeuroTradeAPI.Batch", b =>
                {
                    b.HasOne("NeuroTradeAPI.Instrument", "Instrument")
                        .WithMany()
                        .HasForeignKey("InstrumentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NeuroTradeAPI.Candle", b =>
                {
                    b.HasOne("NeuroTradeAPI.Batch", "Batch")
                        .WithMany()
                        .HasForeignKey("BatchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

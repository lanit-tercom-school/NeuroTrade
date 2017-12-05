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
    [Migration("20171205130218_Rest tables added - fix")]
    partial class Resttablesaddedfix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("NeuroTradeAPI.Algorithm", b =>
                {
                    b.Property<int>("AlgorithmId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("Path");

                    b.Property<int>("UserId");

                    b.HasKey("AlgorithmId");

                    b.HasIndex("UserId");

                    b.ToTable("Algorithms");
                });

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

            modelBuilder.Entity("NeuroTradeAPI.TrainedModel", b =>
                {
                    b.Property<int>("TrainedModelId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AlgorithmId");

                    b.Property<string>("Data");

                    b.Property<int>("InstrumentId");

                    b.Property<string>("Parameters");

                    b.Property<float>("Performance");

                    b.Property<DateTime>("TestBegin");

                    b.Property<DateTime>("TestEnd");

                    b.Property<DateTime>("TrainBegin");

                    b.Property<DateTime>("TrainEnd");

                    b.HasKey("TrainedModelId");

                    b.HasIndex("AlgorithmId");

                    b.HasIndex("InstrumentId");

                    b.ToTable("TrainedModels");
                });

            modelBuilder.Entity("NeuroTradeAPI.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NeuroTradeAPI.Algorithm", b =>
                {
                    b.HasOne("NeuroTradeAPI.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NeuroTradeAPI.Batch", b =>
                {
                    b.HasOne("NeuroTradeAPI.Instrument", "Instrument")
                        .WithMany("RelatedBatches")
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

            modelBuilder.Entity("NeuroTradeAPI.TrainedModel", b =>
                {
                    b.HasOne("NeuroTradeAPI.Algorithm", "Algorithm")
                        .WithMany()
                        .HasForeignKey("AlgorithmId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NeuroTradeAPI.Instrument", "Instrument")
                        .WithMany()
                        .HasForeignKey("InstrumentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

﻿using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NeuroTradeAPI.Entities;

namespace NeuroTradeAPI
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Candle> Candles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Algorithm> Algorithms { get; set; }
        public DbSet<TrainedModel> TrainedModels { get; set; }
        private static DbContextOptions<ApplicationContext> _defaultOptions;

        public ApplicationContext() : base(_defaultOptions)
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            _defaultOptions = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        
        public async Task Initialize()
        {
            Database.EnsureCreated();
            bool nonempty = await Instruments.AnyAsync(); 
            if (nonempty)
                return;
            
            var instruments = new Instrument[]
            {
                new Instrument{InstrumentId=1,Alias="RTS-12.17(RIZ7)",DownloadAlias="SPFB.RTS-12.17"},
                new Instrument{InstrumentId=2,Alias="SBPR-12.17(SPZ7)",DownloadAlias="SPFB.SBPR-12.17"}
            };
            foreach (Instrument instr in instruments)
                Instruments.Add(instr);
            SaveChanges();
            
            var batches = new Batch[]
            {
                // Здесь со временем всё хорошо, поэтому просто Parse. Но вообще используйте tryParse
                new Batch{BatchId=1,InstrumentId=1,Interval=new TimeSpan(1, 0, 0),BeginTime=new DateTime(2017, 11, 1, 10, 0, 0)},
                new Batch{BatchId=2,InstrumentId=2,Interval=new TimeSpan(0, 30, 0),BeginTime=new DateTime(2017, 11, 1, 10, 0, 0)}
            };
            foreach (Batch b in batches)
                Batches.Add(b);
            SaveChanges();

            var candles = new Candle[]
            {
                new Candle{BatchId=1,Open=111700,High=112600,Low=111700,Close=112480,Volume=80244,BeginTime=new DateTime(2017, 11, 1, 10, 0, 0)},
                new Candle{BatchId=1,Open=112470,High=112720,Low=112470,Close=112510,Volume=38690,BeginTime=new DateTime(2017, 11, 1, 11, 0, 0)},
                new Candle{BatchId=2,Open=16047,High=16115,Low=16001,Close=16091,Volume=1751,BeginTime=new DateTime(2017, 11, 1, 10, 0, 0)},
                new Candle{BatchId=2,Open=16091,High=16092,Low=16058,Close=16086,Volume=919,BeginTime=new DateTime(2017, 11, 1, 10, 30, 0)}
            };
            foreach (Candle c in candles)
            {
                Candles.Add(c);
            }
            SaveChanges();
        }
    }
}
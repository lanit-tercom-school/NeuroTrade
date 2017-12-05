﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroTradeAPI
{
    public class TrainedModel
    {
        public int TrainedModelId { get; set; }
        public int AlgorithmId { get; set; }
        public string Parameters { get; set; }
        public string Data { get; set; }
        public float Performance { get; set; }
        public int InstrumentId { get; set; }
        public DateTime TrainBegin { get; set; }
        public DateTime TrainEnd { get; set; }
        public DateTime TestBegin { get; set; }
        public DateTime TestEnd { get; set; }
        [ForeignKey("AlgorithmId")]
        public Algorithm Algorithm { get; set; }
        [ForeignKey("InstrumentId")]
        public Instrument Instrument { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroTradeAPI.Entities
{
    public class Instrument
    {
        public int InstrumentId { get; set; }
        public string Alias { get; set; }
        public string DownloadAlias { get; set; }
        [InverseProperty("Instrument")]
        public List<Batch> RelatedBatches { get; set; }
        
        public Dictionary<string, object> toDict()
        {
            return new Dictionary<string, object>(){
                {"id", InstrumentId},
                {"Alias", Alias},
                {"DownloadAlias", DownloadAlias},
            };
        }
    }
}
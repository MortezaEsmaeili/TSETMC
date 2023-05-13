namespace MSHB.TsetmcReader.DataLayer.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ClientHandler_T
    {
        public long Id { get; set; }

        public long InstrumentId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ReceivedDate { get; set; }

        public long Ibn { get; set; }

        public long Nbn { get; set; }

        public long Ib_nst { get; set; }

        public long Nb_nst { get; set; }

        public long Isn { get; set; }

        public long Nsn { get; set; }

        public long Is_nst { get; set; }

        public long Ns_nst { get; set; }

        public virtual Instrument_T Instrument_T { get; set; }
    }
}

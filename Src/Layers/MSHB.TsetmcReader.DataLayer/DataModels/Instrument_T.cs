namespace MSHB.TsetmcReader.DataLayer.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Instrument_T
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Instrument_T()
        {
            ClientHandler_T = new HashSet<ClientHandler_T>();
            ConfirmedOrder_T = new HashSet<ConfirmedOrder_T>();
            InformationHandler_T = new HashSet<InformationHandler_T>();
        }

        public long Id { get; set; }

        public string InstrumentName { get; set; }

        [StringLength(20)]
        public string InstrumentCode { get; set; }

        public int Signal2 { get; set; }

        public int Signal3 { get; set; }

        public int Signal4 { get; set; }

        public bool IsActive { get; set; }

        public bool ActiveClientData { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientHandler_T> ClientHandler_T { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConfirmedOrder_T> ConfirmedOrder_T { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InformationHandler_T> InformationHandler_T { get; set; }
    }
}

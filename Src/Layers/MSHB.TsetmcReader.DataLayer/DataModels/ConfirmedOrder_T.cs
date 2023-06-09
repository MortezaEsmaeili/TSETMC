namespace MSHB.TsetmcReader.DataLayer.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ConfirmedOrder_T
    {
        public long Id { get; set; }

        public long InstrumentId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ReceivedDate { get; set; }

        public long PurchaseOrderCount_1 { get; set; }

        public long PurchaseOrderVolume_1 { get; set; }

        public long PurchaseOrderPrice_1 { get; set; }

        public long SalesOrderCount_1 { get; set; }

        public long SalesOrderVolume_1 { get; set; }

        public long SalesOrderPrice_1 { get; set; }

        public long PurchaseOrderCount_2 { get; set; }

        public long PurchaseOrderVolume_2 { get; set; }

        public long PurchaseOrderPrice_2 { get; set; }

        public long SalesOrderCount_2 { get; set; }

        public long SalesOrderVolume_2 { get; set; }

        public long SalesOrderPrice_2 { get; set; }

        public long PurchaseOrderCount_3 { get; set; }

        public long PurchaseOrderVolume_3 { get; set; }

        public long PurchaseOrderPrice_3 { get; set; }

        public long SalesOrderCount_3 { get; set; }

        public long SalesOrderVolume_3 { get; set; }

        public long SalesOrderPrice_3 { get; set; }

        public long PurchaseOrderCount_4 { get; set; }

        public long PurchaseOrderVolume_4 { get; set; }

        public long PurchaseOrderPrice_4 { get; set; }

        public long SalesOrderCount_4 { get; set; }

        public long SalesOrderVolume_4 { get; set; }

        public long SalesOrderPrice_4 { get; set; }

        public long PurchaseOrderCount_5 { get; set; }

        public long PurchaseOrderVolume_5 { get; set; }

        public long PurchaseOrderPrice_5 { get; set; }

        public long SalesOrderCount_5 { get; set; }

        public long SalesOrderVolume_5 { get; set; }

        public long SalesOrderPrice_5 { get; set; }

        public double Bv { get; set; }

        public long Nb { get; set; }

        public long Ns { get; set; }

        public double Pb { get; set; }

        public double Ps { get; set; }

        public long Qb { get; set; }

        public long Qs { get; set; }

        public double Sv { get; set; }

        public double Wp { get; set; }

        public long Cp { get; set; }

        public long Ltp { get; set; }

        public double Av15qpcd { get; set; }

        public double Bqpc { get; set; }

        public double Cptqpcd { get; set; }

        public double Qpcd { get; set; }

        public double Sqpc { get; set; }

        public double Ewpcp { get; set; }

        public double Tavewpcp { get; set; }

        public double Cltppbpsdp { get; set; }

        public double Ltppbpsdp { get; set; }

        public double Ltppbpsd { get; set; }

        public virtual Instrument_T Instrument_T { get; set; }
    }
}

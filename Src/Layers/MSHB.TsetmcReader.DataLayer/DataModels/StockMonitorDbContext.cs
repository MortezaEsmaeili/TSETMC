using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MSHB.TsetmcReader.DataLayer.DataModels
{
    public partial class StockMonitorDbContext : DbContext
    {
        public StockMonitorDbContext() : base("name=StockMonitorDbContext") { }

        public virtual DbSet<ClientHandler_T> ClientHandler_T { get; set; }
        public virtual DbSet<ConfirmedOrder_T> ConfirmedOrder_T { get; set; }
        public virtual DbSet<InformationHandler_T> InformationHandler_T { get; set; }
        public virtual DbSet<Instrument_T> Instrument_T { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instrument_T>()
                .HasMany(e => e.ClientHandler_T)
                .WithRequired(e => e.Instrument_T)
                .HasForeignKey(e => e.InstrumentId);

            modelBuilder.Entity<Instrument_T>()
                .HasMany(e => e.ConfirmedOrder_T)
                .WithRequired(e => e.Instrument_T)
                .HasForeignKey(e => e.InstrumentId);

            modelBuilder.Entity<Instrument_T>()
                .HasMany(e => e.InformationHandler_T)
                .WithRequired(e => e.Instrument_T)
                .HasForeignKey(e => e.InstrumentId);
        }
    }
}

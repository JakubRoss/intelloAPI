using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class WarehouseContext :DbContext
    {
        public DbSet<DokumentPrzyjecia> DokumentyPrzyjecia { get; set; }
        public DbSet<Kontrahent> Kontrahenci { get; set; }
        public DbSet<PozycjaDokumentu> PozycjeDokumentow { get; set; }
        public DbSet<Towar> Towar { get; set; }

        public WarehouseContext(DbContextOptions<WarehouseContext> options)
        : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Dokument -> Kontrahent (N:1), bez kaskadowego usuwania kontrahenta
            modelBuilder.Entity<DokumentPrzyjecia>()
                .HasOne(d => d.Kontrahent)
                .WithMany(k => k.DokumentyPrzyjecia)
                .HasForeignKey(d => d.KontrahentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); // Kontrahenta nie można usunąć, jeśli są dokumenty

            // Dokument -> Pozycje (1:N), z kaskadowym usuwaniem pozycji
            modelBuilder.Entity<DokumentPrzyjecia>()
                .HasMany(d => d.Pozycje)
                .WithOne(p => p.DokumentPrzyjecia)
                .HasForeignKey(p => p.DokumentPrzyjeciaId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); // Usunięcie dokumentu usuwa jego pozycje

            // Pozycja -> Towar (N:1), bez możliwości usunięcia towaru, jeśli jest w użyciu
            modelBuilder.Entity<PozycjaDokumentu>()
                .HasOne(p => p.Towar)
                .WithMany(t => t.Pozycje)
                .HasForeignKey(p => p.TowarId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); // Towaru nie można usunąć, jeśli są powiązane pozycje

            base.OnModelCreating(modelBuilder);
        }




    }
}

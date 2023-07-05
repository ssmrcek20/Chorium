using SlojEntiteta.Entiteti;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SlojUpravljanjaSBazomPodataka
{
    public partial class ChoriumModel : DbContext
    {
        public ChoriumModel()
            : base("name=ChoriumModel")
        {
        }

        public virtual DbSet<Aktivnost> Aktivnost { get; set; }
        public virtual DbSet<Kategorija> Kategorija { get; set; }
        public virtual DbSet<Korisnik> Korisnik { get; set; }
        public virtual DbSet<Kucanski_posao> Kucanski_posao { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Tip_korisnika> Tip_korisnika { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aktivnost>()
                .Property(e => e.Naziv)
                .IsUnicode(false);

            modelBuilder.Entity<Aktivnost>()
                .HasMany(e => e.Korisnik)
                .WithMany(e => e.Aktivnost)
                .Map(m => m.ToTable("Korisnik_aktivnost").MapLeftKey("ID_aktivnost").MapRightKey("ID_korisnik"));

            modelBuilder.Entity<Kategorija>()
                .Property(e => e.Naziv)
                .IsUnicode(false);

            modelBuilder.Entity<Kategorija>()
                .HasMany(e => e.Kucanski_posao)
                .WithRequired(e => e.Kategorija)
                .HasForeignKey(e => e.ID_kategorija)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Korisnik>()
                .Property(e => e.Ime)
                .IsUnicode(false);

            modelBuilder.Entity<Korisnik>()
                .Property(e => e.Prezime)
                .IsUnicode(false);

            modelBuilder.Entity<Korisnik>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Korisnik>()
                .Property(e => e.Korisnicko_ime)
                .IsUnicode(false);

            modelBuilder.Entity<Korisnik>()
                .Property(e => e.Lozinka)
                .IsUnicode(false);

            modelBuilder.Entity<Korisnik>()
                .HasMany(e => e.Kucanski_posao)
                .WithRequired(e => e.Korisnik)
                .HasForeignKey(e => e.ID_korisnik_dodao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Korisnik>()
                .HasMany(e => e.Kucanski_posao1)
                .WithMany(e => e.Korisnik1)
                .Map(m => m.ToTable("Korisnik_kucanski_posao").MapLeftKey("ID_korisnik").MapRightKey("ID_kucanski_posao"));

            modelBuilder.Entity<Kucanski_posao>()
                .Property(e => e.Naziv)
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
                .Property(e => e.Naziv)
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Kucanski_posao)
                .WithRequired(e => e.Status)
                .HasForeignKey(e => e.ID_status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tip_korisnika>()
                .Property(e => e.Naziv)
                .IsUnicode(false);

            modelBuilder.Entity<Tip_korisnika>()
                .HasMany(e => e.Korisnik)
                .WithRequired(e => e.Tip_korisnika)
                .HasForeignKey(e => e.ID_tip_korisnika)
                .WillCascadeOnDelete(false);
        }
    }
}

namespace SlojEntiteta.Entiteti
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Korisnik")]
    public partial class Korisnik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Korisnik()
        {
            Kucanski_posao = new HashSet<Kucanski_posao>();
            Aktivnost = new HashSet<Aktivnost>();
            Kucanski_posao1 = new HashSet<Kucanski_posao>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Ime { get; set; }

        [Required]
        [StringLength(50)]
        public string Prezime { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Korisnicko_ime { get; set; }

        [Required]
        [StringLength(100)]
        public string Lozinka { get; set; }

        public int ID_tip_korisnika { get; set; }

        public DateTime Datum_rodenja { get; set; }

        public byte[] Lice { get; set; }
        public byte[] Lice2 { get; set; }
        public byte[] Lice3 { get; set; }
        public byte[] Lice4 { get; set; }
        public byte[] Lice5 { get; set; }

        public virtual Tip_korisnika Tip_korisnika { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kucanski_posao> Kucanski_posao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Aktivnost> Aktivnost { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kucanski_posao> Kucanski_posao1 { get; set; }
    }
}

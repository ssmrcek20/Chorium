namespace SlojEntiteta.Entiteti
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Kucanski_posao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kucanski_posao()
        {
            Korisnik1 = new HashSet<Korisnik>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Naziv { get; set; }

        public DateTime Datum_pocetka { get; set; }

        public DateTime Datum_kraja { get; set; }

        public int ID_status { get; set; }

        public int ID_korisnik_dodao { get; set; }

        public int ID_kategorija { get; set; }

        public virtual Kategorija Kategorija { get; set; }

        public virtual Korisnik Korisnik { get; set; }

        public virtual Status Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Korisnik> Korisnik1 { get; set; }
    }
}

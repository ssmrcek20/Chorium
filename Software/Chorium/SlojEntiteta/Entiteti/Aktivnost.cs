namespace SlojEntiteta.Entiteti
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Aktivnost")]
    public partial class Aktivnost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Aktivnost()
        {
            Korisnik = new HashSet<Korisnik>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Naziv { get; set; }

        public DateTime Datum_pocetka { get; set; }

        public DateTime Datum_kraja { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Korisnik> Korisnik { get; set; }
    }
}

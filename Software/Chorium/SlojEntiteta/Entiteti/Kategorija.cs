namespace SlojEntiteta.Entiteti
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Kategorija")]
    public partial class Kategorija
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kategorija()
        {
            Kucanski_posao = new HashSet<Kucanski_posao>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Naziv { get; set; }

        public int Dobna_granica { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kucanski_posao> Kucanski_posao { get; set; }
    }
}

using SlojEntiteta.Entiteti;
using SlojUpravljanjaSBazomPodataka.repozitoriji;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPoslovneLogike.Servisi
{
    public class KategorijaServis
    {
        public List<Kategorija> DohvatiKategorije()
        {
            using (var repo = new KategorijaRepozitorij())
            {
                return repo.DajSve().ToList();
            }
        }
    }
}

using SlojEntiteta.Entiteti;
using SlojUpravljanjaSBazomPodataka.repozitoriji;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPoslovneLogike.Servisi
{
    public class StatusServis
    {
        public List<Status> DohvatiStatuse()
        {
            using (var repo = new StatusRepozitorij())
            {
                return repo.DajSve().ToList();
            }            
        }
    }
}

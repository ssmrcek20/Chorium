using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojUpravljanjaSBazomPodataka.repozitoriji
{
    public abstract class Repozitori<T> : IDisposable where T : class
    {
        protected ChoriumModel Context { get; set; }

        protected DbSet<T> Entiteti { get; set; }

        public Repozitori(ChoriumModel context)
        {
            Context = context;
            Entiteti = Context.Set<T>();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public virtual IQueryable<T> DajSve()
        {
            var query = from e in Entiteti
                        select e;
            return query;
        }

        public virtual int Dodaj(T entitet)
        {
            Entiteti.Add(entitet);
            return SpremiPromjene();
        }

        public abstract int Azuriraj(T entitet);

        public virtual int Izbrisi(T entitet)
        {
            Entiteti.Attach(entitet);
            Entiteti.Remove(entitet);
            return SpremiPromjene();
        }

        public int SpremiPromjene()
        {
            return Context.SaveChanges();
        }
    }
}

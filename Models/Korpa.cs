using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProdavnica.Models
{
    public class Korpa
    {
        private List<StavkaKorpe> kolekcijaStavki = new List<StavkaKorpe>();
        public  void DodajStavku(Proizvod p, int kolicina)
        {
            StavkaKorpe st1 = kolekcijaStavki
            .SingleOrDefault(st => st.Proizvod.ProizvodId == p.ProizvodId);
            if (st1 == null)
            {
                st1 = new StavkaKorpe
                {
                    Proizvod = p,
                    Kolicina = kolicina
                };
                kolekcijaStavki.Add(st1);
            }
            else
            {
                st1.Kolicina += kolicina;
            }
        }
        public  void ObrisiStavku(int id)
        {
            StavkaKorpe st1 = kolekcijaStavki.SingleOrDefault(st => st.Proizvod.ProizvodId ==
           id);
            kolekcijaStavki.Remove(st1);
        }
        public  void PromeniStavku(Proizvod p, int kolicina)
        {
            StavkaKorpe st1 = kolekcijaStavki.SingleOrDefault(st => st.Proizvod.ProizvodId ==
           p.ProizvodId);
            if (st1 != null)
            {
                st1.Kolicina = kolicina;
            }
        }
        public  decimal VrednostKorpe()
        {
            decimal vrednost = kolekcijaStavki.Sum(p => p.Proizvod.Cena * p.Kolicina);
            return vrednost;
        }
        public virtual void ObrisiKorpu()
        {
            kolekcijaStavki.Clear();
        }
        //public List<StavkaKorpe> Stavke
        //{
        //    get
        //    {
        //        return kolekcijaStavki;
        //    }
        //}

        public List<StavkaKorpe> Stavke => kolekcijaStavki;
    }
}


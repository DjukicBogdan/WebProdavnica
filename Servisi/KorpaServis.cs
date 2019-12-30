using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProdavnica.Ekstenzije;
using WebProdavnica.Models;

namespace WebProdavnica.Servisi
{
    public class KorpaServis
    {
        private readonly IHttpContextAccessor accessor;
        public KorpaServis(IHttpContextAccessor _accessor)
        {
            accessor = _accessor;
        }
        public Korpa CitajKorpu()
        {
            Korpa k = null;
            ISession sesija = accessor.HttpContext.Session;          

            if (sesija.DeserijalizujKorpu("KorpaKljuc") != null)
            {
                k = sesija.DeserijalizujKorpu("KorpaKljuc");
            }
            else
            {
                k = new Korpa();
            }
            return k;
        }
        public void CuvajKorpu(Korpa k)
        {
            ISession sesija = accessor.HttpContext.Session;
            sesija.SerijalizujKorpu("KorpaKljuc", k);
            // accessor.HttpContext.Session.SerijalizujKorpu("KorpaKljuc", k);
        }
        public void ObrisiKorpu()
        {
            accessor.HttpContext.Session.Clear();
        }

    }
}

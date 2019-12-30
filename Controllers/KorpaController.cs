using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebProdavnica.Models;
using WebProdavnica.Servisi;

namespace WebProdavnica.Controllers
{
    public class KorpaController : Controller
    {

        private readonly ProdavnicaContext db;
        private Korpa korpa;
        private KorpaServis kServis;
        public KorpaController(ProdavnicaContext _db, KorpaServis _kServis)
        {
            kServis = _kServis;
            db = _db;
            korpa = kServis.CitajKorpu();
        }
        public IActionResult Index()
        {
            return View(korpa);
        }
        public IActionResult DodajStavku(int ProizvodId)
        {

            Proizvod p1 = db.Proizvodi
            .SingleOrDefault(p => p.ProizvodId == ProizvodId);
            if (p1 != null)
            {
                korpa.DodajStavku(p1, 1);
                kServis.CuvajKorpu(korpa);
            }
            return RedirectToAction("Index");
        }
        public IActionResult ObrisiStavku(int ProizvodId)
        {
            Proizvod p1 = db.Proizvodi
            .SingleOrDefault(p => p.ProizvodId == ProizvodId);
            if (p1 != null)
            {
                korpa.ObrisiStavku(p1.ProizvodId);
                kServis.CuvajKorpu(korpa);
            }
            return RedirectToAction("Index");
        }
        public IActionResult PromeniStavku(int ProizvodId, int kolicina)
        {
            Proizvod proizvod = db.Proizvodi
            .SingleOrDefault(p => p.ProizvodId == ProizvodId);
            if (proizvod != null)
            {
                korpa.PromeniStavku(proizvod, kolicina);
                kServis.CuvajKorpu(korpa);
            }
            return RedirectToAction("Index");
        }
    }
}
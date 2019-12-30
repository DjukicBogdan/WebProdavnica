using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebProdavnica.Models;


namespace WebProdavnica.Ekstenzije
{
    public static class EkstenzijaSesije
    {
        public static void SerijalizujKorpu(this ISession sesija, string kljuc, Korpa k)
        {
            sesija.SetString(kljuc, JsonConvert.SerializeObject(k));
        }
        public static Korpa DeserijalizujKorpu(this ISession sesija, string kljuc)
        {
            string jsonString = sesija.GetString(kljuc);
            if (jsonString != null)
            {
                return JsonConvert.DeserializeObject<Korpa>(jsonString);
            }
            else
            {
                return null;
            }
        }
    }
}

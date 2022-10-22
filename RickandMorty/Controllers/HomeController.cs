using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RickandMorty.Models;
using System.Collections.Generic;
using System.Diagnostics;


namespace RickandMorty.Controllers
{
    public class HomeController : Controller
    {
        private static Characters chars;
        private static Episoden epis;
        private static Standorte orte;

        private HttpClient http;
        private HttpResponseMessage httpresp;
        public HomeController()
        {
            http= new HttpClient();
            httpresp = new HttpResponseMessage();
            //Character holen und anschliessend Gesamtlänge(count) der Response in len_char speichern 
            httpresp = http.GetAsync("https://rickandmortyapi.com/api/character").Result;
            var len_char = (JsonConvert.DeserializeObject<Characters>(httpresp.Content.ReadAsStringAsync().Result));
            chars = JsonConvert.DeserializeObject<Characters>(getObjects(len_char.info.count, "character"));

            //Episoden holen und anschliessend Gesamtlänge(count) der Response in len_epi speichern 
            httpresp = http.GetAsync("https://rickandmortyapi.com/api/episode").Result;
            var len_epi = (JsonConvert.DeserializeObject<Episoden>(httpresp.Content.ReadAsStringAsync().Result));
            epis = JsonConvert.DeserializeObject<Episoden>(getObjects(len_epi.info.count, "episode"));

            //Standorte holen und anschliessend Gesamtlänge(count) der Response in len_ort speichern 
            httpresp = http.GetAsync("https://rickandmortyapi.com/api/location").Result;
            var len_ort = (JsonConvert.DeserializeObject<Standorte>(httpresp.Content.ReadAsStringAsync().Result));
            orte = JsonConvert.DeserializeObject<Standorte>(getObjects(len_ort.info.count, "location"));

        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Charaktere()
        {
         
            List<SelectListItem> spezies = new List<SelectListItem>();
            var spez = chars.root.Select(x => new { x.species }).Distinct().ToList();
            spez.ToList().ForEach(x => spezies.Add(new SelectListItem() { Value = x.species + "", Text = x.species + "" }));
            spezies.Add(new SelectListItem() { Value = "*", Text = "*" });
            ViewBag.spezies = spezies;

            List<SelectListItem> geschlecht = new List<SelectListItem>();
            geschlecht.Add(new SelectListItem() { Value = "*", Text = "*" });
            var geschl = chars.root.Select(x => new { x.gender }).Distinct().ToList();
            geschl.ToList().ForEach(x => geschlecht.Add(new SelectListItem() { Value = x.gender + "", Text = x.gender + "" }));
            ViewBag.geschlecht = geschlecht;

            List<SelectListItem> herkunft = new List<SelectListItem>();
            herkunft.Add(new SelectListItem() { Value = "*", Text = "*" });
            var herknft = chars.root.Select(x => new { x.origin.name }).Distinct().ToList();
            herknft.ToList().ForEach(x => herkunft.Add(new SelectListItem() { Value = x.name + "", Text = x.name + "" }));
            ViewBag.herkunft = herkunft;

            List<SelectListItem> status = new List<SelectListItem>();
            status.Add(new SelectListItem() { Value = "*", Text = "*" });
            var stat = chars.root.Select(x => new { x.status }).Distinct().ToList();
            stat.ToList().ForEach(x => status.Add(new SelectListItem() { Value = x.status + "", Text = x.status + "" }));
            ViewBag.status = status;

            List<SelectListItem> ort = new List<SelectListItem>();
            ort.Add(new SelectListItem() { Value = "*", Text = "*" });
            var vort = chars.root.Select(x => new { x.location.name }).Distinct().ToList();
            vort.ToList().ForEach(x => ort.Add(new SelectListItem() { Value = x.name + "", Text = x.name + "" }));
            ViewBag.ort = ort;

          

            List<SelectListItem> episode = new List<SelectListItem>();
            episode.Add(new SelectListItem() { Value = "*", Text = "*" });
            epis.root.ToList().ForEach(x => episode.Add(new SelectListItem() { Value = x.url, Text = x.id + "" }));
            ViewBag.episode = episode;
            
            return View();
        }

        [HttpGet]
        public IActionResult getCharacters(string spezies, string geschlecht, string herkunft, string status, string ort, string epi)
        {
       
            var chars_ = chars.root.Where(x => (x.species == spezies || spezies == "*") && (x.gender == geschlecht || geschlecht == "*")
            && (x.origin.name == herkunft || herkunft == "*") && (x.status == status || status == "*")
            && (x.location.name == ort || ort == "*") && (x.episode.Contains(epi) || epi == "*")
            ).Select(x => new
            {
                name = x.name,
                species = x.species,
                gender = x.gender,
                origin = x.origin.name,
                epis = x.episode.Count,
                id=x.id,
                image=x.image
            }).ToList();

            return Ok(chars_);
        }

        [HttpGet]
        public IActionResult getCharacter(string id)
        {
            var character = chars.root.Where(x => x.id + "" == id).Select(
                x => new
                {
                    name = x.name,
                    status = x.status,
                    species = x.species,
                    type = x.type,
                    gender = x.gender,
                    location = x.location.name,
                    origin=x.origin.name,
                    image = x.image,
                    epis=epis.root,
                    epi=x.episode
                }).ToList();


            return Ok(character);
        }

        public IActionResult Standorte()
        {
           

            List<SelectListItem> typ = new List<SelectListItem>();
            var typ_ = orte.root.Select(x => new { x.type }).Distinct().ToList();
            typ_.ToList().ForEach(x => typ.Add(new SelectListItem() { Value = x.type + "", Text = x.type + "" }));
            typ.Add(new SelectListItem() { Value = "*", Text = "*" });
            ViewBag.typ = typ;

            List<SelectListItem> dimension = new List<SelectListItem>();
            dimension.Add(new SelectListItem() { Value = "*", Text = "*" });
            var dimension_ = orte.root.Select(x => new { x.dimension }).Distinct().ToList();
            dimension_.ToList().ForEach(x => dimension.Add(new SelectListItem() { Value = x.dimension + "", Text = x.dimension + "" }));
            ViewBag.dimension = dimension;

            return View();
        }
        [HttpGet]
        public IActionResult getStandorte(string typ, string dimension)
        {

            var orte_ = orte.root.Where(x => (x.type == typ || typ == "*") && (x.dimension == dimension || dimension == "*")
            ).Select(x => new
            {
                id = x.id,
                name = x.name,
                typ = x.type,
                dimension=x.dimension,
                residents = x.residents.Count
            }).ToList();

            return Ok(orte_);
        }

        [HttpGet]
        public IActionResult getStandort(string id)
        {
            var ort = orte.root.Where(x => x.id + "" == id).Select(
                x => new
                {
                    name = x.name,
                    typ = x.type,
                    dimension = x.dimension,
                    type = x.type,
                    residents = chars.root,
                    resident = x.residents
                }).ToList();


            return Ok(ort);
        }

        public IActionResult Episoden()
        {
            return View();
        }

        private string getObjects(int len, string model)
        {   //alle Ids in einem Teilstring speichern und anschliessend dem Querystring anfügen 
            string querysubstring = "";
            for (int i = 1; i <= len; i++)
            {
                querysubstring += i + ",";
            }
            querysubstring = querysubstring.Substring(0, querysubstring.Length - 1);
            httpresp = http.GetAsync("https://rickandmortyapi.com/api/" + model + "/" + querysubstring).Result;
            //Objektmodell(Episoden,Charactere..) ein root element im JSON adden  
            string str = "{\"root\"" + ":" + httpresp.Content.ReadAsStringAsync().Result + "}";
            return str;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
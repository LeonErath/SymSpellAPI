using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace SymSpellAPI.Controllers
{

    

    [Route("/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {

        [HttpGet]
        public ActionResult<string> Get()
        {
            return  "API Initialited";
        }

        [HttpPost("lookup/json")]
        public List<SymSpell.SuggestItem> lookupJSON([FromBody]JObject data)
        {
            Console.WriteLine("lookup/json " + data.GetValue("document").Value<string>() +" "+ data.GetValue("distance").Value<int>()+ " "+ data.GetValue("verbosity").Value<int>());
            return SymSpellInterface.Instance.getSuggestions(data.GetValue("document").Value<string>(),  data.GetValue("verbosity").Value<int>(), data.GetValue("distance").Value<int>());
        }

        

        [HttpPost("comound/json")]
        public List<SymSpell.SuggestItem> lookupcompoundJSON([FromBody]JObject data)
        {
            Console.WriteLine("compound/json - document: " + data.GetValue("document").Value<string>());
            return SymSpellInterface.Instance.correctText(data.GetValue("document").Value<string>(), data.GetValue("distance").Value<int>());
        }

        [HttpPost("stemming/json")]
        public (string segmentedString, string correctedString, int distanceSum, decimal probabilityLogSum) wordStemmingJSON([FromBody]JObject data)
        {
            Console.WriteLine("stemming/json - document: " + data.GetValue("document").Value<string>());
            return SymSpellInterface.Instance.segmentText(data.GetValue("document").Value<string>(), data.GetValue("distance").Value<int>());
        }
    

        [HttpPost("lookup/formdata")]
        public ActionResult<List<SymSpell.SuggestItem>> lookup()
        {
            string document = Request.Form["document"];
            int distance;
            int.TryParse(Request.Form["distance"],out distance);
            int verbosity;
            int.TryParse(Request.Form["verbosity"],out verbosity);
            return SymSpellInterface.Instance.getSuggestions(document,verbosity,distance);

        }

        [HttpPost("compound/formdata")]
        public ActionResult<List<SymSpell.SuggestItem>> lookupcompound()
        {
            string document = Request.Form["document"];
            int distance;
            int.TryParse(Request.Form["distance"],out distance);
            return SymSpellInterface.Instance.correctText(document,distance);
        }

        [HttpPost("stemming/formdata")]
        public ActionResult<(string segmentedString, string correctedString, int distanceSum, decimal probabilityLogSum)> wordstemming()
        {
            string document = Request.Form["document"];
            int distance;
            int.TryParse(Request.Form["distance"],out distance);
            return SymSpellInterface.Instance.segmentText(document,distance);
        }
    }
}

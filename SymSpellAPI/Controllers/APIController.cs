using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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


        [HttpPost("lookup")]
        public ActionResult<List<SymSpell.SuggestItem>> lookup()
        {
            string document = Request.Form["document"];
            int distance;
            int.TryParse(Request.Form["distance"],out distance);
            int verbosity;
            int.TryParse(Request.Form["verbosity"],out verbosity);
            return SymSpellInterface.Instance.getSuggestions(document,verbosity,distance);

        }

        [HttpPost("lookupcompound")]
        public ActionResult<List<SymSpell.SuggestItem>> lookupcompound()
        {
            string document = Request.Form["document"];
            int distance;
            int.TryParse(Request.Form["distance"],out distance);
            return SymSpellInterface.Instance.correctText(document,distance);
        }

        [HttpPost("wordstemming")]
        public ActionResult<(string segmentedString, string correctedString, int distanceSum, decimal probabilityLogSum)> wordstemming()
        {
            string document = Request.Form["document"];
            int distance;
            int.TryParse(Request.Form["distance"],out distance);
            return SymSpellInterface.Instance.segmentText(document,distance);
        }
    }
}

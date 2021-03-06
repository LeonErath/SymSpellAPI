﻿using System;
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

        [HttpGet("lookup")]
        public List<SymSpell.SuggestItem> lookup([FromQuery(Name = "document")] string document, [FromQuery(Name = "distance")] int distance, [FromQuery(Name = "verbosity")] int verbosity)
        {
            try
            {
                return SymSpellInterface.Instance.getSuggestions(document, verbosity, distance);

            }
            catch (Exception e)
            {
           
                return new List<SymSpell.SuggestItem>();
            }
        }

        [HttpPost("lookup/json")]
        public List<SymSpell.SuggestItem> lookupJSON([FromBody]JObject data)
        {
            try
            {
                return SymSpellInterface.Instance.getSuggestions(data.GetValue("document").Value<string>(), data.GetValue("verbosity").Value<int>(), data.GetValue("distance").Value<int>());

            }catch(Exception e)
            {
                Console.WriteLine(e.Message,data);
                return new List<SymSpell.SuggestItem>();
            }
        }

        

        [HttpPost("compound/json")]
        public List<SymSpell.SuggestItem> lookupcompoundJSON([FromBody]JObject data)
        {
           
            return SymSpellInterface.Instance.correctText(data.GetValue("document").Value<string>(), data.GetValue("distance").Value<int>());
        }

        [HttpPost("stemming/json")]
        public (string segmentedString, string correctedString, int distanceSum, decimal probabilityLogSum) wordStemmingJSON([FromBody]JObject data)
        {
            
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

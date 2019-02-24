using Microsoft.AspNetCore.Mvc;
using QtaElastic.Models;
using QtaElastic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QtaElastic.Controllers
{
    [Route("api/[controller]")]
    public class QtaController : Controller
    {
        private IElasticApi _elasticApi;
        public QtaController(IElasticApi elasticApi)
        {
            _elasticApi = elasticApi;
        }
        private static QtaData[] SampleData = new[]
        {
           new QtaData(){Phrase ="laser"}, new QtaData(){Phrase ="system"}, new QtaData(){Phrase ="water"}
        };

        [HttpGet("[action]")]
        public async Task<IEnumerable<QtaData>> QtaDatas(string query)
        {
            //return SampleData;
            return await _elasticApi.GetSuggestions(query);
        }
    }
}

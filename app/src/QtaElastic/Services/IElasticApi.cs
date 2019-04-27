using QtaElastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QtaElastic.Services
{
   public interface IElasticApi
    {
        Task<IEnumerable<QtaData>> GetSuggestions(string query, string docid);
        Task<string[]> AvailableDocuments();
    }
}

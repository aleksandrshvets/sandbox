using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QtaElastic.Models;

namespace QtaElastic.Services
{
    public class ElasticApi : IElasticApi
    {
        private readonly IOptions<AppConfig> _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public ElasticApi(IOptions<AppConfig> config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<QtaData>> GetSuggestions(string query, string docid)
        {
            var elasticUrl = Environment.GetEnvironmentVariable("ELASTIC_BASE_URL");
            if (string.IsNullOrEmpty(elasticUrl))
            {
                elasticUrl = _config.Value.ElasticUrl;
            }

            var client = _httpClientFactory.CreateClient();
            //var elasticQuery = string.Format(
            //    @"
            //    {{
            //        ""suggest"": {{
            //            ""my-suggest-1"" : {{
            //                ""prefix"" : ""{0}"",
            //                ""completion"" : {{
            //                    ""field"" : ""phraseSuggest"",
            //                    ""skip_duplicates"": true,
            //                    ""fuzzy"": {{
            //                        ""fuzziness"": 1
            //                        }}
            //                    }}
            //                }}
            //            }}
            //        }}
            //    }}", query);

            var elasticQuery = string.Format(
                @"
                {{
                  ""suggest"": {{
                    ""my-suggest-1"" : {{
                      ""prefix"" : ""{0}"",
                      ""completion"" : {{
                        ""field"" : ""caption_suggest"",
                        ""skip_duplicates"": true,
                        ""contexts"": {{
                                    ""global_id"": [ ""{1}""]
                                }}
                      }}
                    }}
                  }}
                }}", query, docid);

            var savedQuery = await GetSuggestionQuery();
            if (!string.IsNullOrEmpty(savedQuery))
            {
                elasticQuery = string.Format(savedQuery, query);
            }
            var result = await client.PostAsync(string.Format("{0}{1}", elasticUrl, "/qta-poc/_search"), new StringContent(elasticQuery, Encoding.UTF8, "application/json"));

            var responseStr = await result.Content.ReadAsStringAsync();
            var parsedResponse = ParseResponse(responseStr);
            var suggestions = parsedResponse["suggest"]["my-suggest-1"].Children() as IEnumerable<JToken>;

            var resultList = new List<QtaData>();

            foreach (var item in suggestions)
            {
                var options = item["options"].Children() as IEnumerable<JToken>;

                var lQuery = from option in options
                             select new QtaData() {
                                 Text = option["text"].ToString(),
                                 Caption = option["_source"]["caption"].ToString(),
                                 Page = option["_source"]["page"].ToString(),
                                 Weight = option["_source"]["weight"].ToString(),
                             };
                resultList.AddRange(lQuery.ToList());
            }



            return resultList;
        }

        private async Task<string> GetSuggestionQuery()
        {
            try
            {

            var elasticUrl = Environment.GetEnvironmentVariable("ELASTIC_BASE_URL");
            if (string.IsNullOrEmpty(elasticUrl))
            {
                elasticUrl = _config.Value.ElasticUrl;
            }

            var client = _httpClientFactory.CreateClient();

            var elasticQuery =
                @"
                    {
                      ""query"": {
                            ""bool"": {
                                ""must"": {
                                  ""term"":{
                                  ""id"":1
                                }
                            }}
                      }
                    }";
            var result = await client.PostAsync(string.Format("{0}{1}", elasticUrl, "qta-poc-meta/_search"), new StringContent(elasticQuery, Encoding.UTF8, "application/json"));
            var responseStr = await result.Content.ReadAsStringAsync();
            var parsedResponse = ParseResponse(responseStr);
            var hits = parsedResponse["hits"]["hits"].Children() as IEnumerable<JToken>;
            foreach (var item in hits)
            {
                var data = item["_source"]["data"];
                return data.ToString();
            }
            }
            catch (Exception)
            {

                return null;
            }

            return null;
        }

        private JObject ParseResponse(string response)
        {
            if (response == string.Empty) throw new Exception("No response to parse");
            var json = JsonConvert.DeserializeObject<dynamic>(response);
            var str = Convert.ToString(json);
            return JObject.Parse(str);
        }
    }
}

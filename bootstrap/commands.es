

PUT qta-poc
{
        "mappings" : {
        "doc" : {
          "properties" : {
            "pageRank" : {
              "type" : "long"
            },
            "pages" : {
              "type" : "text",
              "fields" : {
                "keyword" : {
                  "type" : "keyword"
                }
              }
            },
            "phrase" : {
              "type" : "text",
              "fields" : {
                "keyword" : {
                  "type" : "keyword"
                }
              }
            },
            "phraseSuggest" : {
              "type" : "completion"
            }
          }
        }
      }
}

DELETE qta-poc





POST qta-poc/_search
{
  "suggest": {
    "my-suggest-1" : {
      "prefix" : "GENERAL",
      "completion" : {
        "field" : "phraseSuggest",
        "skip_duplicates": true
      }
    }
  }
}

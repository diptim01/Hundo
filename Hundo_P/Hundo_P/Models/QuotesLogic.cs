using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Hundo_P.Models
{
    public class QuotesLogic
    {
        public static async Task<List<Quotes>> GetQuotesAsync()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.BaseAddress = new System.Uri("https://andruxnet-random-famous-quotes.p.mashape.com/");
            //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("X-Mashape-Key", "AqLfRVfG2QmshemxINrQCLcJIT6sp1qA8OYjsn1nwLkuOHE0Pz");
            httpClient.DefaultRequestHeaders.Add("X-Mashape-Key", "AqLfRVfG2QmshemxINrQCLcJIT6sp1qA8OYjsn1nwLkuOHE0Pz");
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var r = await httpClient.GetAsync("?cat=famous&count=12");

            r.EnsureSuccessStatusCode();
            string response = await r.Content.ReadAsStringAsync();
            //var hello = JObject.Parse(response);
            var jsonRes =  JsonConvert.DeserializeObject<List<Quotes>>(response);
            return jsonRes;

        }

        public class Quotes
        {
            public string quote { get; set; }
            public string author { get; set; }
            public string category { get; set; }

        }
    }

}
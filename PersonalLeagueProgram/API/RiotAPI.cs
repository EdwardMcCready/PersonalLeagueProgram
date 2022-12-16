using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PersonalLeagueProgram.API
{
    public class RiotAPI
    {
        private string Key { get; set; }
        private string Region { get; set; }

        public RiotAPI(string key, string region)
        {
            Key = key;
            Region = region;
        }

        protected T SendRequest<T>(string path)
        {
            var response = SendRequest(GetUri(path));

            if (response?.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync().Result;

                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
            }

            return default(T);
        }

        protected HttpResponseMessage SendRequest(string URL)
        {
            using (HttpClient client = new HttpClient())
            {
                var result = client.GetAsync(URL);
                result.Wait();

                return result.Result;
            }
        }

        protected string GetUri(string request)
        {
            return $"https://{Region}.api.riotgames.com/lol/{request}/?api_key={Key}";
        }

    }
}

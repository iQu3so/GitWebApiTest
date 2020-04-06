using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;


namespace GitWebApiTest
{

    public class Repository
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static async Task ProcessRespositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);
            foreach (var repo in repositories)
            {
                Console.WriteLine(repo.Name);
            }
        }


        static async Task Main(string[] args)
        {
            await ProcessRespositories();
        }
    }
}


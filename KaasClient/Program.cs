using KaasClient.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace KaasClient {
    class Program {
        static async Task Main() {
            Console.Write("Smaak:");
            var smaak = Console.ReadLine();
            using var client = new HttpClient();
            var response =
            await client.GetAsync($"http://localhost:5000/kazen/smaken?smaak={smaak}");
            switch (response.StatusCode) {
                case HttpStatusCode.OK:
                    var kazen = await response.Content.ReadAsAsync<List<Kaas>>();
                    kazen.ForEach(kaas => Console.WriteLine(kaas.Naam));
                    break;
                case HttpStatusCode.NotFound:
                    Console.WriteLine("Kazen niet gevonden");
                    break;
                default:
                    Console.WriteLine("Technisch probleem, contacteer de helpdesk.");
                    break;
            }
        }
    }
}
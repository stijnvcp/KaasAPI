using KaasService.Repositories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using Moq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using KaasService.Models;
using Newtonsoft.Json;
using System.Text.Json;

namespace KaasServiceIntegrationTests {
    [TestClass]
    public class KaasControllerTest {
        private HttpClient client;
        private Mock<IKaasRepository> mock;
        [TestInitialize]
        public void TestInitialize() {
            mock = new Mock<IKaasRepository>();
            var repository = mock.Object;
            var factory = new WebApplicationFactory<KaasService.Startup>();
            client = factory.WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services =>
            services.AddTransient<IKaasRepository>(_ => repository)))
            .CreateClient();
        }
        [TestMethod]
        public void GetMetOnbestaandeKaasGeeftNotFound() {
            var response = client.GetAsync("kazen/1").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            mock.Verify(repo => repo.FindByIdAsync(1));
        }
        [TestMethod]
        public void GetMetBestaandeKaasGeeftOK() {
            mock.Setup(repo => repo.FindByIdAsync(1)).Returns(Task.FromResult(
            new Kaas() { Id = 1, Naam = "1", Soort = "1", Smaak = "1" }));
            var response = client.GetAsync("kazen/1").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            mock.Verify(repo => repo.FindByIdAsync(1));
            var body = response.Content.ReadAsStringAsync().Result;
            var bodyInJson = JsonDocument.Parse(body);
            Assert.AreEqual(1, (int)bodyInJson.RootElement.GetProperty("id").GetInt32());
            Assert.AreEqual("1", (string)bodyInJson.RootElement.GetProperty("naam").GetString());
        }
    }
}
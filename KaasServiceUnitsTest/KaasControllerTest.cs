using KaasService.Controllers;
using KaasService.Models;
using KaasService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace KaasServiceUnitTests {
    [TestClass]
    public class KaasControllerTest {
        private KaasController controller;
        private Mock<IKaasRepository> mock;
        private Kaas kaas7;
        private Kaas kaas9;
        [TestInitialize]
        public void Initialize() {
            mock = new Mock<IKaasRepository>();
            var repository = mock.Object;
            controller = new KaasController(repository);
            kaas7 = new Kaas { Id = 7, Naam = "7", Smaak = "7", Soort = "7" };
            kaas9 = new Kaas { Id = 9, Naam = "9", Smaak = "9", Soort = "9" };
        }
        [TestMethod]
        public void FindbyIdGeeftNotFoundBijOnbestaandeKaas() {
            Assert.IsInstanceOfType(controller.FindById(7).Result, typeof(NotFoundResult));
            mock.Verify(repo => repo.FindByIdAsync(7));
        }
        [TestMethod]
        public void FindByIdGeeftBestaandeKaasTerug() {
            mock.Setup(repo => repo.FindByIdAsync(7)).Returns(Task.FromResult(kaas7));
            var response = (OkObjectResult)controller.FindById(7).Result;
            var kaas = (Kaas)response.Value;
            Assert.AreEqual(7, kaas.Id);
            mock.Verify(repo => repo.FindByIdAsync(7));
        }
        [TestMethod]
        public void FindAllGeeftAlleKazenTerug() {
            var alleKazen = new List<Kaas>() { kaas7, kaas9 };
            mock.Setup(repo => repo.FindAllAsync()).Returns(Task.FromResult(alleKazen));
            var response = (OkObjectResult)controller.FindAll().Result;
            var kazen = (List<Kaas>)response.Value;
            Assert.AreEqual(2, kazen.Count);
            Assert.AreEqual(7, kazen[0].Id);
            Assert.AreEqual(9, kazen[1].Id);
            mock.Verify(repo => repo.FindAllAsync());
        }
        [TestMethod]
        public void FindBySmaakGeeftJuisteKazenTerug() {
            var kazenMetSmaak7 = new List<Kaas>() { kaas7 };
            mock.Setup(repo =>
            repo.FindBySmaakAsync("7")).Returns(Task.FromResult(kazenMetSmaak7));
            var response = (OkObjectResult)controller.FindBySmaak("7").Result;
            var kazen = (List<Kaas>)response.Value;
            Assert.AreEqual(1, kazen.Count);
            Assert.AreEqual(7, kazen[0].Id);
            mock.Verify(repo => repo.FindBySmaakAsync("7"));
        }
        [TestMethod]
        public void PutGeeftNotFoundBijOnbestaandeKaas() {
            mock.Setup(repo => repo.UpdateAsync(kaas7))
            .Throws(new DbUpdateConcurrencyException());
            Assert.IsInstanceOfType(controller.Put(7, kaas7).Result,
            typeof(NotFoundResult));
            mock.Verify(repo => repo.UpdateAsync(kaas7));
        }
        [TestMethod]
        public void PutWijzigtKaas() {
            Assert.IsInstanceOfType(controller.Put(7, kaas7).Result, typeof(OkResult));
            mock.Verify(repo => repo.UpdateAsync(kaas7));
        }
    }
}

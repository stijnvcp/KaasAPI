using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaasService.Repositories;
using KaasService.Models;
using Microsoft.EntityFrameworkCore;

namespace KaasService.Controllers
{
    [Route("kazen")]
    [ApiController]
    public class KaasController : ControllerBase
    {
        private readonly IKaasRepository repository;
        public KaasController(IKaasRepository repository) => this.repository = repository;

        [HttpGet]
        public ActionResult FindAll() => base.Ok(repository.FindAll());

        [HttpGet("{id}")]
        public ActionResult FindById(int id) {
            var kaas = repository.FindById(id);
            return kaas == null ? base.NotFound() : base.Ok(kaas);
        }

        [HttpGet("smaken")]
        public ActionResult FindBySmaak(string smaak) =>
            base.Ok(repository.FindBySmaak(smaak));

        [HttpPut("{id}")]
        public ActionResult Put(int id, Kaas kaas) {
            if (this.ModelState.IsValid) {
                try {
                    kaas.Id = id;
                    repository.Update(kaas);
                    return base.Ok();
                }
                catch (DbUpdateConcurrencyException) {
                    return base.NotFound();
                }
                catch {
                    return base.Problem();
                }
            }
            return base.BadRequest();
        }
    }
}

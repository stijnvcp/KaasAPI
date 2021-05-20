using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaasService.Models;

namespace KaasService.Repositories
{
    public interface IKaasRepository
    {
        IQueryable<Kaas> FindAll();
        Kaas FindById(int id);
        IQueryable<Kaas> FindBySmaak(string smaak);
        void Update(Kaas kaas);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaasService.Models;

namespace KaasService.Repositories
{
    public interface IKaasRepository
    {
        Task<List<Kaas>> FindAllAsync();
        Task<Kaas> FindByIdAsync(int id);
        Task<List<Kaas>> FindBySmaakAsync(string smaak);
        Task UpdateAsync(Kaas kaas);

    }
}

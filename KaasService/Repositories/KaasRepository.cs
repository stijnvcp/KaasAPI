using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaasService.Models;
using Microsoft.EntityFrameworkCore;

namespace KaasService.Repositories
{
    public class KaasRepository : IKaasRepository
    {
        private readonly KaaslandContext context;
        public KaasRepository(KaaslandContext context) => this.context = context;
        public async Task<List<Kaas>> FindAllAsync() =>
            await context.Kazen.AsNoTracking().ToListAsync();
        public async Task<Kaas> FindByIdAsync(int id) =>
            await context.Kazen.FindAsync(id);
        public async Task<List<Kaas>> FindBySmaakAsync(string smaak) =>
            await context.Kazen.AsNoTracking().Where(kaas => kaas.Smaak==smaak).ToListAsync();
        public async Task UpdateAsync(Kaas kaas) {
            context.Kazen.Update(kaas);
            await context.SaveChangesAsync();
        }

    }
}

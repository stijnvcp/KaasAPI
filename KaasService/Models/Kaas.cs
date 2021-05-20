using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaasService.Models
{
    public class Kaas
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Soort { get; set; }
        public string Smaak { get; set; }
    }
}

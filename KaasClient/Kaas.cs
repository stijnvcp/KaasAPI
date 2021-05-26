using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace KaasClient.Models {
    [DataContract(Name = "kaas", Namespace = "")]
    public class Kaas {
        [DataMember(Name = "id", Order = 1)]
        public int Id { get; set; }
        [DataMember(Name = "naam", Order = 2)]
        public string Naam { get; set; }
        [DataMember(Name = "soort", Order = 3)]
        public string Soort { get; set; }
        [DataMember(Name = "smaak", Order = 4)]
        public string Smaak { get; set; }
    }
}


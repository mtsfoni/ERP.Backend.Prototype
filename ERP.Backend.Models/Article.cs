using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Backend.Models
{
    [DataContract]
    public class Article
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; } = string.Empty;

        [DataMember(Order = 3)]
        public string Brand { get; set; } = string.Empty;

        [DataMember(Order = 4)]
        public List<Price> Prices { get; set; } = [];
    }
}

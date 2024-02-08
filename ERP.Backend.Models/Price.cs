using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ERP.Backend.Models
{
    [DataContract]
    public class Price
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ArticleId { get; set; }
        [DataMember]
        public DateTime ValidFrom { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public Article? Article { get; set; }
    }
}

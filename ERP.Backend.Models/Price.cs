using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DataMember(Order = 1)]
        [Key]        
        public int Id { get; set; }
        [DataMember(Order = 2)]
        [ForeignKey(nameof(Article))]
        public int ArticleId { get; set; }
        [DataMember(Order = 3)]
        public DateTime ValidFrom { get; set; }
        [DataMember(Order = 4)]
        public decimal Amount { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public Article? Article { get; set; }
    }
}

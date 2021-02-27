using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace seventh.Models
{
    [Table("server")]
    public class Server
    {
        [Key]
        [Column("id")]
        [JsonIgnore]
        public string Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("ip")]
        [Required]
        public string Ip { get; set; }

        [Column("porta")]
        [Required]
        public int Port { get; set; }
        public ICollection<Videos> Videos { get; set; }

        public bool ShouldSerializeVideos()
        {
            return Videos != null;
        }
    }
}
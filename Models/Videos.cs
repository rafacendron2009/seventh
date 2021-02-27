using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace seventh.Models
{
    [Table("videos")]
    public class Videos
    {
        [Key]
        [Column("id")]
        [JsonIgnore]
        public string Id { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("video")]
        public string VideoBase64 { get; set; }

         [Column("size")]
        public decimal sizeInBytes { get; set; }

        [Column("serverId")]
        public string ServerId { get; set; }

    }
}
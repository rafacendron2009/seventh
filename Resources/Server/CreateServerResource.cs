using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace seventh.Resources
{
    public class CreateServerResource
    {

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Ip { get; set; }

        [Required]
        public int Port { get; set; }

    }
}
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace seventh.Resources
{
    public class CreateVideoResource
    {
        public string Descricao { get; set; }
        public string VideoBase64 { get; set; }
    }
}
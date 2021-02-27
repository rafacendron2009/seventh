using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace seventh.Models
{
    public class ServerResource
    {
        public string Id { get; set; }

        public string Nome { get; set; }

        public string Ip { get; set; }

        public int Port { get; set; }

    }
}
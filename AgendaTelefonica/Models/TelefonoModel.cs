using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgendaTelefonica.Models
{
    [NotMapped]
    public class TelefonoModel: Telefono
    {
        public string Numero2 { get; set; }
        public string TipoTelefono2 { get; set; }
    }
}
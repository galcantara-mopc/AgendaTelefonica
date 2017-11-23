using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgendaTelefonica.DTO
{
    public class TelefonoDTO
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string TipoTelefono { get; set; }
        public string Cedula { get; set; }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaTelefonica.Models
{
    public class Telefono
    {
        public int Id { get; set; }
        public string TipoTelefono { get; set; }
        public string Numero { get; set; }
        public string Cedula { get; set; }
        public Persona Persona { get; set; }
    }
}

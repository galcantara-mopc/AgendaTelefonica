using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgendaTelefonica.Models
{
    [NotMapped]
    public class PersonaModel:Persona
    {
        public Array telefonos { get; set; }
    }
}
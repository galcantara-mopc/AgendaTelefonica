using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaTelefonica.Models
{
    public class Direccion
    {
        public int Id { get; set; }
        public string Pais { get; set; }
        public string DireccionP { get; set; }
        public virtual Persona Persona { get; set; }

    }
}

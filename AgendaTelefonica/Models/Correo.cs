using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AgendaTelefonica.Models
{
    public class Correo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public virtual Persona Persona { get; set; }
    }
}

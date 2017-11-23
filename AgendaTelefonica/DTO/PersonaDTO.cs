using AgendaTelefonica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgendaTelefonica.DTO
{
    public class PersonaDTO
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public string DireccionP { get; set; }
        public string Pais { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AgendaTelefonica.Models;
using System.Data.SqlClient;
using AgendaTelefonica.DTO;
using AgendaTelefonica.Reportes;
using AgendaTelefonica.BOL;

namespace AgendaTelefonica.Controllers
{
    [RoutePrefix("api/data")]
    public class DataController : ApiController
    {
        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();
        private CRUD _operacion = new CRUD();
        //METODOS POST
        [HttpPost]
        [Route("guardarpersona")]
        public bool GudardarPersona([FromBody] PersonaModel persona)
        {
            var _persona = new Persona()
            {
                Cedula = persona.Cedula,
                Nombre = persona.Nombre.ToUpper(),
                Apellidos = persona.Apellidos.ToUpper(),
                Genero = persona.Genero,
                FechaNacimiento = persona.FechaNacimiento,
                Direccion = new Direccion()
                {
                    Pais = persona.Direccion.Pais,
                    DireccionP = persona.Direccion.DireccionP
                },
                Correo = new Correo()
                {
                    Email = persona.Correo.Email

                },
                Telefonos =persona.Telefonos

             };
            var repuesta=_operacion.Agregar(_persona);
            return repuesta;
        }
        [HttpPost]
        [Route("agregartelefono")]
        public bool GuardarTelefono([FromBody] TelefonoModel telefono)
        {
            var _telefono = new Telefono()
            {
                Numero = telefono.Numero2,
                TipoTelefono = telefono.TipoTelefono2,
                Id = telefono.Id
            };
            var repuesta =_operacion.Agregar(_telefono);
            return repuesta;   
        }
        [HttpPost]
        [Route("editarpersona")]
        public bool EditarPersona([FromBody] PersonaModel persona)
        {
            var _persona = new Persona()
            {
                Cedula = persona.Cedula,
                Nombre = persona.Nombre.ToUpper(),
                Apellidos = persona.Apellidos.ToUpper(),
                Genero = persona.Genero,
                FechaNacimiento = persona.FechaNacimiento,
                Direccion = new Direccion()
                {
                    Pais = persona.Direccion.Pais,
                    DireccionP = persona.Direccion.DireccionP
                },
                Correo = new Correo()
                {
                    Email = persona.Correo.Email

                },
                Telefonos =persona.Telefonos

             };
            var repuesta=_operacion.Agregar(_persona);
            return repuesta;
        }
        [HttpPost]
        [Route("editartelefono")]
        public bool EditarTelefono([FromBody] TelefonoModel telefono)
        {
            var _telefono = new Telefono()
            {
                Numero = telefono.Numero,
                TipoTelefono = telefono.TipoTelefono,
                Id = telefono.Id
            };
            var repuesta = _operacion.Editar(_telefono);
            return repuesta;
        }
        [HttpPost]
        [Route("eliminarpersona")]
        public bool EliminarPersona([FromBody] PersonaModel persona)
        {
            var _persona = new Persona()
            {
                Cedula = persona.Cedula,
            };
            var resultado = _operacion.Eliminar(_persona);
            return resultado;
        }
        [HttpPost]
        [Route("eliminartelefono")]
        public bool EliminarTelefono([FromBody] TelefonoModel telefono)
        {
            var _telefono = new Telefono()
            {
                Id = telefono.Id
            };
            var repuesta = _operacion.Eliminar(_telefono);
            return repuesta;
        }
        [HttpPost]
        [Route("generarreporte")]
        //public List<Persona> GenerarReporte([FromBody] PersonaModel persona)
        //{
        //    //int repuesta = 0;
        //    //var listPersona = new List<Persona>();
        //    //var telefonos = new List<Telefono>();
        //    //telefonos = persona.Telefonos.ToList();
        //    //var perso = new Persona()
        //    //{
        //    //    Cedula = persona.Cedula,
        //    //    Nombre = persona.Nombre,
        //    //    Apellidos = persona.Apellidos,
        //    //    FechaNacimiento = persona.FechaNacimiento,
        //    //    Genero = persona.Genero,
        //    //    Correo = new Correo()
        //    //    {
        //    //        Email = persona.Correo.Email
        //    //    },
        //    //    Direccion = new Direccion()
        //    //    {
        //    //        DireccionP = persona.Direccion.DireccionP,
        //    //        Pais = persona.Direccion.Pais
        //    //    },
        //    //    Telefonos = telefonos

        //    //};
        //    //listPersona.Add(perso);



        //    return listPersona;

        //}
        //METODOS GET
        [HttpGet]
        [Route("buscarpersonas")]
        public IEnumerable<PersonaDTO> BuscarPersonas()
        {

            var query = from p in _dbContext.Personas
                        join c in _dbContext.Correos on p.Id equals c.Id
                        join d in _dbContext.Direcciones on p.Id equals d.Id
                        orderby p.Id ascending
                        select new PersonaDTO
                        {
                            Cedula = p.Cedula,
                            Nombre = p.Nombre,
                            Apellidos = p.Apellidos,
                            Genero = p.Genero,
                            FechaNacimiento = p.FechaNacimiento,
                            Correo = c.Email,
                            DireccionP = d.DireccionP,
                            Pais= d.Pais
                        };

            return query;
        }
        [HttpGet]
        [Route("buscartelefonos")]
        //public IEnumerable<Telefono>
        public IEnumerable<TelefonoDTO> BuecarTelefonos()
        {
            var query = from p in _dbContext.Personas
                        join t in _dbContext.Telefonos on p.Cedula equals t.Cedula
                        orderby p.Id ascending
                        select new TelefonoDTO
                        {
                            Id = t.Id,
                            Numero = t.Numero,
                            TipoTelefono = t.TipoTelefono,
                            Cedula = t.Cedula
                        };

            return query;
        }
        
    }
}

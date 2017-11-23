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

namespace AgendaTelefonica.Controllers
{
    [RoutePrefix("api/data")]
    public class DataController : ApiController
    {
        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();
        //METODOS POST
        [HttpPost]
        [Route("guardarpersona")]
        public bool GudardarPersona([FromBody] PersonaModel persona)
        {
            int repuesta;
            var _per = new Persona()
            {
                Cedula = persona.Cedula,
                Nombre = persona.Nombre.ToUpper(),
                Apellidos = persona.Apellidos.ToUpper(),
                Genero = persona.Genero,
                FechaNacimiento = persona.FechaNacimiento

            };
            var _direc = new Direccion()
            {
                Pais = persona.Direccion.Pais,
                DireccionP = persona.Direccion.DireccionP
            };

            var _cor = new Correo()
            {
                Email = persona.Correo.Email

            };

            var _Tele = new Telefono();

            _dbContext.Personas.Add(_per);
            foreach (var i in persona.Telefonos)
            {
                _Tele.Cedula = _per.Cedula;
                _Tele.TipoTelefono = i.TipoTelefono;
                _Tele.Numero = i.Numero;
                _dbContext.Telefonos.Add(_Tele);
                _dbContext.SaveChanges();
            }

            _dbContext.Direcciones.Add(_direc);
            _dbContext.Correos.Add(_cor);
            repuesta = _dbContext.SaveChanges();


            return repuesta > 0;
        }
        [HttpPost]
        [Route("agregartelefono")]
        public bool GuardarTelefono([FromBody] TelefonoModel telefono)
        {
            var cedula = _dbContext.Telefonos.Find(telefono.Id);
            var tele = new Telefono();
            int resultado = 0;
            tele.Numero = telefono.Numero2;
            tele.TipoTelefono = telefono.TipoTelefono2;
            tele.Cedula = cedula.Cedula;
            _dbContext.Telefonos.Add(tele);
            resultado = _dbContext.SaveChanges();

            return resultado > 0;   

        }
        [HttpPost]
        [Route("editarpersona")]
        public bool EditarPersona([FromBody] PersonaModel persona)
        {
            int resultado = 0;
            var pers = _dbContext.Personas.First(p => p.Cedula == persona.Cedula);
           
            var cor = _dbContext.Correos.First(c => c.Id == pers.Id);
            var dir = _dbContext.Direcciones.First(d => d.Id == pers.Id);

            if (pers.Nombre != persona.Nombre)
            {
                pers.Nombre = persona.Nombre;
                _dbContext.Entry(pers).State = System.Data.Entity.EntityState.Modified;
                resultado = _dbContext.SaveChanges();
            }
            if (pers.Apellidos != persona.Apellidos)
            {
                pers.Apellidos = persona.Apellidos;
                _dbContext.Entry(pers).State = System.Data.Entity.EntityState.Modified;
                resultado = _dbContext.SaveChanges();
            }
            if(pers.Genero != persona.Genero)
            {
                pers.Genero = persona.Genero;
                _dbContext.Entry(pers).State = System.Data.Entity.EntityState.Modified;
                resultado = _dbContext.SaveChanges();
            }
            if (cor.Email != persona.Correo.Email)
            {
                cor.Email = persona.Correo.Email;
                _dbContext.Entry(cor).State = System.Data.Entity.EntityState.Modified;
                resultado = _dbContext.SaveChanges();
            }
            if (dir.Pais != persona.Direccion.Pais)
            {
                dir.Pais = persona.Direccion.Pais;
                _dbContext.Entry(dir).State = System.Data.Entity.EntityState.Modified;
                resultado = _dbContext.SaveChanges();
            }
            if (dir.DireccionP != persona.Direccion.DireccionP)
            {
                dir.DireccionP = persona.Direccion.DireccionP;
                _dbContext.Entry(dir).State = System.Data.Entity.EntityState.Modified;
                resultado = _dbContext.SaveChanges();   
            }
            
            return resultado > 0;
        }
        [HttpPost]
        [Route("eliminarpersona")]
        public bool EliminarPersona([FromBody] PersonaModel persona)
        {
            var pers = _dbContext.Personas.First(p => p.Cedula == persona.Cedula);
            var tel = _dbContext.Telefonos.Where( p => p.Cedula== persona.Cedula);
            var cor = _dbContext.Correos.First(c => c.Id == pers.Id);
            var dir = _dbContext.Direcciones.First(d => d.Id == pers.Id);
            foreach(var i in tel)
            {
                _dbContext.Telefonos.Remove(i);
            }
            //_dbContext.Entry(tel).State = System.Data.Entity.EntityState.Deleted;
            _dbContext.Entry(cor).State = System.Data.Entity.EntityState.Deleted;
            _dbContext.Entry(dir).State = System.Data.Entity.EntityState.Deleted;
            _dbContext.Entry(pers).State = System.Data.Entity.EntityState.Deleted;

            var resultado = _dbContext.SaveChanges();
            return resultado > 0;
        }
        [HttpPost]
        [Route("eliminartelefono")]
        public bool EliminarTelefono([FromBody] TelefonoModel telefono)
        {

            var tel = _dbContext.Telefonos.Find(telefono.Id);
            _dbContext.Entry(tel).State = System.Data.Entity.EntityState.Deleted;
            var repuesta = _dbContext.SaveChanges();
            return repuesta > 0;
        }
        [HttpPost]
        [Route("editartelefono")]
        public bool EditarTelefono([FromBody] TelefonoModel telefono)
        {
            int resultado = 0;
            var tel = _dbContext.Telefonos.Find(telefono.Id);
            if (tel.Numero != telefono.Numero)
            {
                tel.Numero = telefono.Numero;
                _dbContext.Entry(tel).State = System.Data.Entity.EntityState.Modified;
                resultado = _dbContext.SaveChanges();
            }
            else if (tel.TipoTelefono != telefono.TipoTelefono)
            {
                tel.TipoTelefono = telefono.TipoTelefono;
                _dbContext.Entry(tel).State = System.Data.Entity.EntityState.Modified;
                resultado = _dbContext.SaveChanges();
            }

            return resultado > 0;
        }
        [HttpPost]
        [Route("generarreporte")]
        public List<Persona> GenerarReporte([FromBody] PersonaModel persona)
        {
            int repuesta = 0;
            var listPersona = new List<Persona>();
            var telefonos = new List<Telefono>();
            telefonos = persona.Telefonos.ToList();
            var perso = new Persona()
            {
                Cedula = persona.Cedula,
                Nombre = persona.Nombre,
                Apellidos = persona.Apellidos,
                FechaNacimiento = persona.FechaNacimiento,
                Genero = persona.Genero,
                Correo = new Correo()
                {
                    Email = persona.Correo.Email
                },
                Direccion = new Direccion()
                {
                    DireccionP = persona.Direccion.DireccionP,
                    Pais = persona.Direccion.Pais
                },
                Telefonos = telefonos

            };
            listPersona.Add(perso);



            return listPersona;

        }
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

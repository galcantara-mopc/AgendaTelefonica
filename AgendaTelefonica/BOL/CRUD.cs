using AgendaTelefonica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgendaTelefonica.BOL
{
    public class CRUD
    {

        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();
        private readonly Persona _persona = new Persona();
        private readonly Telefono _telefono = new Telefono();
        private readonly Direccion _direccion = new Direccion();
        private readonly Correo _correo = new Correo();
        public bool Agregar(object obj)
        {
            int repuesta = 0;
            if(obj.GetType() == _persona.GetType())
            {
                var persona = (Persona)obj;
                
                _dbContext.Personas.Add(persona);
                
                _dbContext.Correos.Add(persona.Correo);
                _dbContext.Direcciones.Add(persona.Direccion);

                var _Tele = new Telefono();
                foreach (var i in persona.Telefonos)
                {
                    _Tele.Cedula = persona.Cedula;
                    _Tele.TipoTelefono = i.TipoTelefono;
                    _Tele.Numero = i.Numero;
                    _dbContext.Telefonos.Add(_Tele);
                    _dbContext.SaveChanges();
                }
                repuesta = _dbContext.SaveChanges();
            }
            else 
            {
                var telefono = (Telefono)obj;
                var cedula = _dbContext.Telefonos.Find(telefono.Id);
                telefono.Cedula = cedula.Cedula;
                _dbContext.Telefonos.Add(telefono);
                repuesta = _dbContext.SaveChanges();
            }
            return repuesta>0;
        }
        public bool Editar(object obj)
        {
            int repuesta = 0;
            
            
            if (obj.GetType() == _persona.GetType())
            {
                var persona = (Persona)obj;
                var pers = _dbContext.Personas.First(p => p.Cedula == persona.Cedula);
                var cor = _dbContext.Correos.First(c => c.Id == pers.Id);
                var dir = _dbContext.Direcciones.First(d => d.Id == pers.Id);

                if (pers.Nombre != persona.Nombre)
                {
                    pers.Nombre = persona.Nombre;
                    _dbContext.Entry(pers).State = System.Data.Entity.EntityState.Modified;
                    repuesta = _dbContext.SaveChanges();
                }
                if (pers.Apellidos != persona.Apellidos)
                {
                    pers.Apellidos = persona.Apellidos;
                    _dbContext.Entry(pers).State = System.Data.Entity.EntityState.Modified;
                    repuesta = _dbContext.SaveChanges();
                }
                if (pers.Genero != persona.Genero)
                {
                    pers.Genero = persona.Genero;
                    _dbContext.Entry(pers).State = System.Data.Entity.EntityState.Modified;
                    repuesta = _dbContext.SaveChanges();
                }
                if (cor.Email != persona.Correo.Email)
                {
                    cor.Email = persona.Correo.Email;
                    _dbContext.Entry(cor).State = System.Data.Entity.EntityState.Modified;
                    repuesta = _dbContext.SaveChanges();
                }
                if (dir.Pais != persona.Direccion.Pais)
                {
                    dir.Pais = persona.Direccion.Pais;
                    _dbContext.Entry(dir).State = System.Data.Entity.EntityState.Modified;
                    repuesta = _dbContext.SaveChanges();
                }
                if (dir.DireccionP != persona.Direccion.DireccionP)
                {
                    dir.DireccionP = persona.Direccion.DireccionP;
                    _dbContext.Entry(dir).State = System.Data.Entity.EntityState.Modified;
                    repuesta = _dbContext.SaveChanges();
                }
            }
            else
            {
                var telefono = (Telefono)obj;
                var tel = _dbContext.Telefonos.Find(telefono.Id);
                
                if(tel.Numero != telefono.Numero)
                {
                    tel.Numero = telefono.Numero;
                    _dbContext.Entry(tel).State = System.Data.Entity.EntityState.Modified;
                    repuesta = _dbContext.SaveChanges();
                }
                if(tel.TipoTelefono != telefono.TipoTelefono)
                {
                    tel.TipoTelefono = telefono.TipoTelefono;
                    _dbContext.Entry(tel).State = System.Data.Entity.EntityState.Modified;
                    repuesta = _dbContext.SaveChanges();
                }

            }


            return repuesta>0;
        }
        public bool Eliminar(object obj)
        {
            int repuesta = 0;
            if (obj.GetType() == _persona.GetType())
            {
                var persona = (Persona)obj;
                var pers = _dbContext.Personas.First(p => p.Cedula == persona.Cedula);
                var tel = _dbContext.Telefonos.Where(p => p.Cedula == persona.Cedula);
                var cor = _dbContext.Correos.First(c => c.Id == pers.Id);
                var dir = _dbContext.Direcciones.First(d => d.Id == pers.Id);
                _dbContext.Entry(cor).State = System.Data.Entity.EntityState.Deleted;
                _dbContext.Entry(dir).State = System.Data.Entity.EntityState.Deleted;
                _dbContext.Entry(pers).State = System.Data.Entity.EntityState.Deleted;
                foreach (var i in tel)
                {
                    _dbContext.Telefonos.Remove(i);
                }
                _dbContext.Entry(cor).State = System.Data.Entity.EntityState.Deleted;
                _dbContext.Entry(dir).State = System.Data.Entity.EntityState.Deleted;
                _dbContext.Entry(pers).State = System.Data.Entity.EntityState.Deleted;

                var resultado = _dbContext.SaveChanges();
                return resultado > 0;
            }
            else
            {
                var telefono = (Telefono)obj;
                var tele = _dbContext.Telefonos.Find(telefono.Id);
                _dbContext.Entry(tele).State = System.Data.Entity.EntityState.Deleted;
                repuesta = _dbContext.SaveChanges();
            }
            return repuesta>0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ApiAlumnos.Models;

namespace ApiAlumnos.Controllers
{
    public class AlumnoController : ApiController
    {
        private CursosEntities db;

        public AlumnoController()
        {
            db = new CursosEntities();
        }

        public IQueryable<Alumno> Get()
        {
            return db.Alumno;
        }

        [ResponseType(typeof(Alumno))]

        public IHttpActionResult GetPorId(int id)
        {
            var data = db.Alumno.Find(id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [ResponseType(typeof (Alumno))]

        public IHttpActionResult Get(int id)
        {
            var a = db.Alumno.Find(id);
            if (a==null)
            {
                return NotFound();
            }
            else
            {
                return Ok(a);
            }
        }


        [ResponseType(typeof(Alumno))]

        public ICollection<Alumno> GetPorCurso(string nombreCurso)
        {
            var data = db.Curso.Include("Alumno").FirstOrDefault(o => o.Nombre.Contains(nombreCurso));
            if (data == null)
            {
                return null;
            }
            return data.Alumno;
        }

        public ICollection<Alumno> GetPorProfesor(int idProfesor)
        {
            var c = db.ProfesorCurso.Where(o => o.idProfesor == idProfesor).Select(o => o.idCurso);

            var al = db.Curso.Where(o => c.Contains(o.idCurso)).Select(o => o.Alumno);
            var l=new List<Alumno>();
            foreach (var a in al)
            {
                l.AddRange(a);
            }

            return l;
        }


        [ResponseType(typeof(Alumno))]

        public IHttpActionResult PostAlumno(Alumno alumno)
        {
            db.Alumno.Add(alumno);

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest("Error en el alta");
            }

            return Created("ApiAlumnos", alumno);
        }

        [ResponseType(typeof (Alumno))]

        public IHttpActionResult Put(Alumno al)
        {
            //La mejor versión, mas rápida y breve, es esta:
            db.Entry(al).State=EntityState.Modified;
            //De esta forma es él mismo el que hace la búsqueda por clave
            //primaria y luego actualizar campo a campo.

            //var alu = db.Alumno.Find(al.DNI);
            //if (alu == null)
            //{
            //    return NotFound();
            //}
            //alu.Nombre = al.Nombre;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {

                return BadRequest();
            }
            return Ok(al);
        }

        [ResponseType(typeof(Alumno))]

        public IHttpActionResult Delete(int id)
        {

            var alu= db.Alumno.Find(id);

            if (alu==null)
            {
                return NotFound();
            }
            db.Alumno.Remove(alu);

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {

                return BadRequest();
            }
            return Ok();
        }

    }
}

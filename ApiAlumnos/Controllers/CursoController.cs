using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ApiAlumnos.Models;

namespace ApiAlumnos.Controllers
{
    public class CursoController : ApiController
    {
        private CursosEntities db;

        public CursoController()
        {
            db=new CursosEntities();
        }

        public IQueryable<Curso> Get()
        {
            return db.Curso;
        }
        
        [ResponseType(typeof (Curso))]

        public IHttpActionResult GerPotId(int id)
        {
            var data = db.Curso.Find(id);

            if (data==null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }
    }
}

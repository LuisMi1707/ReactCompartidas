﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reactBackend.Models;
using reactBackend.Repository;

namespace WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private AlumnoDAO _dao = new AlumnoDAO();

        #region endPointAlumnoProfesor
        [HttpGet("alumnoProfesor")]

        public List<AlumnoProfesor> GetAlumnoProfesors(string usuario) 
        {
            return _dao.alumnoProfesors(usuario);
            
        }
        #endregion

        #region SelectByID
        [HttpGet("alumno")]
        public Alumno selecrById(int id)
        {
            var alumno = _dao.GetById(id);
            return alumno;
        }
        #endregion

        #region ActualizarDatos
        [HttpPut("alumno")]

        public bool actualizarAlumno([FromBody] Alumno alumno)
        {
            return _dao.update(alumno.Id, alumno);
        }
        #endregion

        #region AlumnoMatricula
        [HttpPost("alumno")]
        public bool insertarMatricula([FromBody] Alumno alumno, int idAsignatura)
        {
            return _dao.InsertarMatricula(alumno, idAsignatura);
        }
        #endregion

        #region EliminarAlumno
        [HttpDelete("alumno")]

        public bool eliminarAlumno(int id)
        {
            return _dao.eliminarAlumno(id);
        }
        #endregion
    }
}

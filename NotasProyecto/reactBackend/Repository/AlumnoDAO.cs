using Microsoft.EntityFrameworkCore.Query;
using reactBackend.Context;
using reactBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reactBackend.Repository
{
    public class AlumnoDAO
    {
        #region Contex

        public RegistroAlumnoContext contexto = new RegistroAlumnoContext();
        #endregion

        #region Select All
        public List<Alumno> SelectAll()
        {
            // -> creamos una variable  var que es generica 
            // -> el contexto tiene referecniada todos los modelos. 
            // -> dentro den EF tenemos  el metodo modelo.ToList<Modelo>
            var alumno = contexto.Alumnos.ToList<Alumno>();
            return alumno;
        }

        #endregion

        #region Select por ID
        public Alumno? GetById(int id)
        {
            var alumno = contexto.Alumnos.Where(x => x.Id == id).FirstOrDefault();
            return alumno == null ? null : alumno;
        }
        #endregion

        #region Insertar Alumno
        public bool insertarAlumno(Alumno alumno)
        {
            try
            {
                var alum = new Alumno
                {
                    Direccion = alumno.Direccion,
                    Edad = alumno.Edad,
                    Email = alumno.Email,
                    Dni = alumno.Dni
                };

                contexto.Alumnos.Add(alum);

                contexto.SaveChanges();

                return true;
            }

            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region UpdateAlumno
        public bool update(int id, Alumno actualizar)
        {
            try
            {
                var alumnoUpdate = GetById(id);
                if (alumnoUpdate == null)
                {
                    Console.WriteLine("Alumno es null");
                    return false;
                }

                alumnoUpdate.Direccion = actualizar.Direccion;
                alumnoUpdate.Dni = actualizar.Dni;
                alumnoUpdate.Nombre = actualizar.Nombre;
                alumnoUpdate.Email = actualizar.Email;

                contexto.Alumnos.Update(alumnoUpdate);
                contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException); 
                return false;
            }
        }
        #endregion

        #region DeleteAlumno
        public bool deleteAlumno(int id)
        {
            var borrar = GetById(id);
            try
            {
                if (borrar != null)
                {
                    return false;
                }
                else
                {
                    contexto.Alumnos.Remove(borrar);
                    contexto.SaveChanges();
                    return true;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine (e.InnerException);
                return false;
            }
        }
        #endregion

        #region leftJoinAlumnoAsignatura
        public List<AlumnoAsignatura> SelectAlumAsig()
        {
            var consulta = from a in contexto.Alumnos
                join m in contexto.Matriculas on a.Id equals m.AlumnoId
                join asig in contexto.Asignaturas on m.AsignaturaId equals asig.Id
                  select new AlumnoAsignatura
                  {
                      nombreAlumno = a.Nombre,
                        nombreAsignatura = asig.Nombre
                  };
            return consulta.ToList();
        }
        #endregion

        #region leftJoinAlumnoProfesor
        public List<AlumnoProfesor> alumnoProfesors(string nombreProfesor)
        {
            var listadoALumno = from a in contexto.Alumnos
                                join m in contexto.Matriculas on a.Id equals m.AlumnoId
                                join asig in contexto.Asignaturas on m.AsignaturaId equals asig.Id
                                where asig.Profesor == nombreProfesor
                                select new AlumnoProfesor
                                {
                                    Id = a.Id,
                                    Dni = a.Dni,
                                    Nombre = a.Nombre,
                                    Direccion = a.Direccion,
                                    Edad = a.Edad,
                                    Email = a.Email,
                                    asignatura = asig.Nombre
                                };

            return listadoALumno.ToList();
        }
        #endregion
    }

}

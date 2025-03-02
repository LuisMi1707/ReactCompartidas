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

        #region SelccionarPorDni
        /// <summary>
        /// Este metodo devolvera null si no exiate el DNI indicado, recibe un alumno y apartir de el se extrae el DNI se devuelve el estudiandiante en caso de exito
        /// </summary>
        /// <param name="alumno"> es de tipo Alumno </param>
        /// <returns> Alumno </returns>
        public Alumno DNIAlumno(Alumno alumno)
        {
            var alumnos = contexto.Alumnos.Where(x => x.Dni == alumno.Dni).FirstOrDefault();
            return alumnos == null ? null : alumnos;
        }
        #endregion

        #region AlumnoMatricula
        public bool InsertarMatricula(Alumno alumno, int idAsing)
        {
            // se utiliza  un bloque con el cual  detectaremos las exepciones que nos pueda dar la inserccion 
            try
            {

                //comprobar si existe el DNI en los alumnos
                var alumnoDNI = DNIAlumno(alumno);
                //si existe solo lo añadimos pero si no lo debemos de insertar
                if (alumnoDNI == null)
                {
                    insertarAlumno(alumno);
                    // si en null creamos el alumno pero ahora debemos de matricular el alumno con el Dni que corresponda
                    var alumnoInsertado = DNIAlumno(alumno);
                    // ahora debemos crear un objeto matricula para poder hacer la insercion de ambas llaves
                    var unirAlumnoMatricula = matriculaAsignaturaALumno(alumno, idAsing);
                    if (unirAlumnoMatricula == false)
                    {
                        return false;
                    }

                    return true;
                }
                else
                {
                    matriculaAsignaturaALumno(alumnoDNI, idAsing);
                    return true;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #region Matriucla
        /// <summary>
        /// Relaciona el Id del alumno con el ID de la matricula 
        /// se definel el id de la asignatura
        /// Para ello el metodo crea una instancia de Matricula he inicializa los campos idAlumno e id Asignatura
        /// si el registro se guarda  devuelve true de lo contrario False
        /// </summary>
        /// <param name="alumno"></param>
        /// <param name="idAsignatura"></param>
        /// <returns>  bool</returns>
        public bool matriculaAsignaturaALumno(Alumno alumno, int idAsignatura)
        {
            try
            {
                Matricula matricula = new Matricula();
                //usaremos los campos AlumnoId y asignaturaId
                matricula.AlumnoId = alumno.Id;
                matricula.AsignaturaId = idAsignatura;
                // Guardamos el cambio que se realizo al momento de insertar.
                contexto.Matriculas.Add(matricula);
                contexto.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion
    }
}



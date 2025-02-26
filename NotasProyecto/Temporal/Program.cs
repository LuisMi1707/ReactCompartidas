using reactBackend.Models;
using reactBackend.Repository;
using System.ComponentModel.DataAnnotations;

AlumnoDAO alumnoDAO = new AlumnoDAO();
#region SelectAll
var alumno = alumnoDAO.SelectAll();

foreach (var item in alumno)
{
    Console.WriteLine(item.Nombre);
}
#endregion
Console.WriteLine(" ");
#region SelectByID
var selectById = alumnoDAO.GetById(1);
Console.WriteLine(selectById?.Nombre);
#endregion
Console.WriteLine(" ");
#region AddAlumno
var nuevoAlumno = new Alumno
{
    Direccion = "Cahalatenango, Nueva Trinidad",
    Dni = "54321",
    Edad = 19,
    Email = "54321@email",
    Nombre = "Luis Miguel Abrego"
};
var resultado = alumnoDAO.insertarAlumno(nuevoAlumno);
Console.WriteLine(resultado);
#endregion

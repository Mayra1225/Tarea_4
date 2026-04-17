using backend.Models;
using backend.DTOs;

public class AlumnoService : IAlumnoService
{
    private static List<Alumno> alumnos = new List<Alumno>
    {
        new Alumno { Id = 1, Codigo = "A001", Nombre="Juan", Apellido="Perez", P1 = 7, P2 = 5 },
        new Alumno { Id = 2, Codigo = "A002", Nombre="Maria", Apellido="Lopez", P1 = 4, P2 = 6 }
    };

    public List<Alumno> GetAll() => alumnos;

    public Alumno GetById(int id) => alumnos.FirstOrDefault(a => a.Id == id);

    public Alumno Create(AlumnoCreateDTO dto)
    {
        var alumno = new Alumno
        {
            Id = alumnos.Count + 1,
            Codigo = dto.Codigo,
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            P1 = dto.P1,
            P2 = dto.P2
        };

        alumnos.Add(alumno);
        return alumno;
    }

    public Alumno Update(int id, AlumnoUpdateDTO dto)
    {
        var alumno = GetById(id);
        if (alumno == null) return null;

        alumno.Nombre = dto.Nombre;
        alumno.Apellido = dto.Apellido;
        alumno.P1 = dto.P1;
        alumno.P2 = dto.P2;

        return alumno;
    }

    public bool Delete(int id)
    {
        var alumno = GetById(id);
        if (alumno == null) return false;

        alumnos.Remove(alumno);
        return true;
    }

    public double CalcularNecesario(double p1, double p2)
    {
        return (6 - (p1 * 0.25 + p2 * 0.35)) / 0.40;
    }

    public double CalcularFinal(double p1, double p2)
    {
        var p3 = CalcularNecesario(p1, p2);
        return p1 * 0.25 + p2 * 0.35 + p3 * 0.40;
    }
}
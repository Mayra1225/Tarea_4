using backend.Models;
using backend.DTOs;

public interface IAlumnoService
{
    List<Alumno> GetAll();
    Alumno GetById(int id);
    Alumno Create(AlumnoCreateDTO dto);
    Alumno Update(int id, AlumnoUpdateDTO dto);
    bool Delete(int id);
    double CalcularNecesario(double p1, double p2);
    double CalcularFinal(double p1, double p2);
}
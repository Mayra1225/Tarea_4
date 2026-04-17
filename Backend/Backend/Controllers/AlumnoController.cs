using Microsoft.AspNetCore.Mvc;
using backend.DTOs;

[ApiController]
[Route("api/[controller]")]
public class AlumnoController : ControllerBase
{
    private readonly IAlumnoService _service;

    public AlumnoController(IAlumnoService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var data = _service.GetAll().Select(a => new
        {
            a.Id,
            a.Codigo,
            a.Nombre,
            a.Apellido,
            a.P1,
            a.P2,
            Necesario = _service.CalcularNecesario(a.P1, a.P2),
            Final = _service.CalcularFinal(a.P1, a.P2)
        });

        return Ok(data);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var a = _service.GetById(id);
        if (a == null) return NotFound();

        return Ok(new
        {
            alumno = a,
            necesario = _service.CalcularNecesario(a.P1, a.P2)
        });
    }

    [HttpPost]
    public IActionResult Create(AlumnoCreateDTO dto)
    {
        return Ok(_service.Create(dto));
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, AlumnoUpdateDTO dto)
    {
        var result = _service.Update(id, dto);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_service.Delete(id)) return NotFound();
        return Ok();
    }
}
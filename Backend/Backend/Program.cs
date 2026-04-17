var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IAlumnoService, AlumnoService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("cors_app", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Alumnos v1");
    c.RoutePrefix = "swagger"; 
});

app.UseRouting();

app.UseCors("cors_app");

app.UseAuthorization();

app.MapControllers();

app.Run();
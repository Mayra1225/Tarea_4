var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("all",
        policy => policy
            .WithOrigins("https://tarea4ingweb.netlify.app/")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build(); 


app.UseCors("all");

app.UseAuthorization();

app.MapControllers();

app.Run();
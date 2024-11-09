using Microsoft.EntityFrameworkCore;
using challenge_c_sharp.Config;
using challenge_c_sharp.Repositories;
using challenge_c_sharp.Models;
using challenge_c_sharp.Services;
using challenge_c_sharp.Dtos;

var builder = WebApplication.CreateBuilder(args);

// 1. Registra os serviços do MVC, incluindo controllers e views
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// 2. Configuração do banco de dados e log
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OracleDbContext"))
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine, LogLevel.Information);
});

// 3. Registra os repositórios com suas interfaces
builder.Services.AddScoped<IGenericRepository<DentistaDto>, DentistaRepository>();
builder.Services.AddScoped<DentistaService>();
builder.Services.AddScoped<IGenericRepository<PacienteDto>, PacienteRepository>();
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<IGenericRepository<ConsultaDto>, ConsultaRepository>();
builder.Services.AddScoped<ConsultaService>();
builder.Services.AddScoped<IGenericRepository<LoginDto>, LoginRepository>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<IGenericRepository<EnderecoDto>, EnderecoRepository>();
builder.Services.AddScoped<EnderecoService>();
builder.Services.AddScoped<IGenericRepository<BairroDto>, BairroRepository>();
builder.Services.AddScoped<BairroService>();
builder.Services.AddScoped<IGenericRepository<CidadeDto>, CidadeRepository>();
builder.Services.AddScoped<CidadeService>();
builder.Services.AddScoped<IGenericRepository<EstadoDto>, EstadoRepository>();
builder.Services.AddScoped<EstadoService>();
builder.Services.AddScoped<IGenericRepository<UnidadeDto>, UnidadeRepository>();
builder.Services.AddScoped<UnidadeService>();

// 4. Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader());
});

// 5. Construir o aplicativo
var app = builder.Build();

// 6. Usar a política CORS configurada
app.UseCors("AllowAll");

// 7. Definindo a rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=HomeWeb}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=EstadoWeb}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=PacienteWeb}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ConsultaWeb}/{action=Index}/{id?}");

// 8. Usar HTTPS, autorização e mapear os controllers
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// 9. Iniciar o aplicativo
app.Run();

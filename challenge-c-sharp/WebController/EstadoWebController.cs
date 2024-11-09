using Microsoft.AspNetCore.Mvc;
using challenge_c_sharp.Dtos;
using challenge_c_sharp.Services;

namespace challenge_c_sharp.Controllers
{
    [ApiController]
    [Route("Estado")]  // Aqui está a rota personalizada, mas ela será sobrescrita pela configuração no Program.cs
    public class EstadoWebController : Controller
    {
        private readonly EstadoService _estadoService;

        public EstadoWebController(EstadoService estadoService)
        {
            _estadoService = estadoService;
        }

        // Ação de listar os estados
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var estados = await _estadoService.GetEstadosAsync();
            return View(estados);  // Retorna a view com os estados
        }

        // Ação de criar um novo estado
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] EstadoDto estadoDto)
        {
            if (estadoDto == null)
            {
                return BadRequest("Dados inválidos ou ausentes.");
            }

            // Aqui você pode validar o estadoDto antes de salvar no banco
            if (ModelState.IsValid)
            {
                await _estadoService.AddEstadoAsync(estadoDto);
                return Ok(new { message = "Estado criado com sucesso!" });
            }

            return BadRequest(ModelState);  // Retorna os erros de validação, se houver
        }


        // Ação de editar um estado
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var estado = await _estadoService.GetEstadoByIdAsync(id);
            if (estado == null)
            {
                return NotFound();
            }
            return View(estado);
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, EstadoDto estadoDto)
        {
            if (id != estadoDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _estadoService.UpdateEstadoAsync(id, estadoDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(estadoDto);
            }
        }

        // Ação de excluir um estado
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var estado = await _estadoService.GetEstadoByIdAsync(id);
            if (estado == null)
            {
                return NotFound();
            }
            return View(estado);
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _estadoService.DeleteEstadoAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

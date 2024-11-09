using challenge_c_sharp.Dtos;
using challenge_c_sharp.Services;
using Microsoft.AspNetCore.Mvc;

namespace challenge_c_sharp.WebController
{
    [Route("pacientes")]
    public class PacienteWebController : Controller
    {
        private readonly PacienteService _pacienteService;

        public PacienteWebController(PacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        // GET: /pacientes
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pacientes = await _pacienteService.GetPacientesAsync();
            return View(pacientes);
        }

        // GET: /pacientes/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /pacientes/create
        [HttpPost("create")]
        public async Task<IActionResult> Create(PacienteDto pacienteDto)
        {
            if (ModelState.IsValid)
            {
                await _pacienteService.AddPacienteAsync(pacienteDto);
                return RedirectToAction(nameof(Index));
            }
            return View(pacienteDto);
        }

        // GET: /pacientes/edit/{id}
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var paciente = await _pacienteService.GetPacienteByIdAsync(id);
            if (paciente == null) return NotFound();

            return View(paciente);
        }

        // POST: /pacientes/edit/{id}
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, PacienteDto pacienteDto)
        {
            if (id != pacienteDto.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                await _pacienteService.UpdatePacienteAsync(pacienteDto);
                return RedirectToAction(nameof(Index));
            }

            return View(pacienteDto);
        }

        // GET: /pacientes/delete/{id}
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var paciente = await _pacienteService.GetPacienteByIdAsync(id);
            if (paciente == null) return NotFound();

            return View(paciente);
        }

        // POST: /pacientes/delete/{id}
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _pacienteService.DeletePacienteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

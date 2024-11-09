using challenge_c_sharp.Dtos;
using challenge_c_sharp.Services;
using Microsoft.AspNetCore.Mvc;

namespace challenge_c_sharp.WebController
{
    [Route("dentistas")]
    public class DentistaWebController : Controller
    {
        private readonly DentistaService _dentistaService;

        public DentistaWebController(DentistaService dentistaService)
        {
            _dentistaService = dentistaService;
        }

        // Exibir a lista de dentistas
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dentistas = await _dentistaService.GetDentistasAsync();
            return View(dentistas);
        }

        
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // Salvar o novo dentista
        [HttpPost("create")]
        public async Task<IActionResult> Create(DentistaDto dentistaDto)
        {
            if (ModelState.IsValid)
            {
                await _dentistaService.AddDentistaAsync(dentistaDto);
                return RedirectToAction(nameof(Index));
            }

            return View(dentistaDto);
        }

        // Exibir o formulário para editar um dentista existente
        [HttpGet("{id}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var dentista = await _dentistaService.GetDentistaByIdAsync(id);
            if (dentista == null)
            {
                return NotFound();
            }

            return View(dentista);
        }

        // Atualizar os dados do dentista
        [HttpPost("{id}/edit")]
        public async Task<IActionResult> Edit(int id, DentistaDto dentistaDto)
        {
            if (id != dentistaDto.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _dentistaService.UpdateDentistaAsync(dentistaDto);
                return RedirectToAction(nameof(Index));
            }

            return View(dentistaDto);
        }

        // Exibir a página de confirmação de exclusão
        [HttpGet("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var dentista = await _dentistaService.GetDentistaByIdAsync(id);
            if (dentista == null)
            {
                return NotFound();
            }

            return View(dentista);
        }

        // Deletar um dentista
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _dentistaService.DeleteDentistaAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

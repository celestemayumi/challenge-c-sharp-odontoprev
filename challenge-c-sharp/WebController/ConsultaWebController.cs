using challenge_c_sharp.Dtos;
using challenge_c_sharp.Services;
using Microsoft.AspNetCore.Mvc;

namespace challenge_c_sharp.WebController
{
    [Route("consultas")]
    public class ConsultaWebController : Controller
    {
        private readonly ConsultaService _consultaService;

        public ConsultaWebController(ConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        // Exibir lista de consultas
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var consultas = await _consultaService.GetConsultasAsync();
            return View(consultas);
        }

    }
}

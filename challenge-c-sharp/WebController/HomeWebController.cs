using Microsoft.AspNetCore.Mvc;

public class HomeWebController : Controller
{
    public IActionResult Index()
    {
        return View(); 
    }
}

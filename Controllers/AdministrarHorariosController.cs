using Microsoft.AspNetCore.Mvc;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class AdministrarHorariosController : Controller
    {
        private readonly AppDbContext _context;
        public AdministrarHorariosController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var Personas = _context.tblPersonas.ToList();
            return View(Personas);
        }

        public IActionResult MarcarHorario()
        {
            var Personas = _context.tblPersonas.ToList();
            return View(Personas);
        }


        public IActionResult MarcarEntrada()
        {
            var FechaHora = DateTime.Now;
            var P = _context.tblPersonas.FirstOrDefault(p => p.Rut.Equals(User.Identity.Name));
            var H = new Horario()
            {
                PersonaId = P.Id,
                HoraMarcacion = FechaHora,
                Tipo = "Entrada"
            };
            _context.Add(H);
            _context.SaveChanges();

            return RedirectToAction("LoginIn", "Auth");
        }


        public IActionResult MarcarSalida()
        {
            var FechaHora = DateTime.Now;
            var P = _context.tblPersonas.FirstOrDefault(p => p.Rut.Equals(User.Identity.Name));
            var H = new Horario()
            {
                PersonaId = P.Id,
                HoraMarcacion = FechaHora,
                Tipo = "Salida"
            };
            _context.Add(H);
            _context.SaveChanges();

            return RedirectToAction("LoginIn", "Auth");
        }

    }
}

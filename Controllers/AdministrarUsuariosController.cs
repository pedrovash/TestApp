using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using TestApp.Models;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    public class AdministrarUsuariosController : Controller
    {
        private readonly AppDbContext _context;
        public AdministrarUsuariosController(AppDbContext context)
        {
            _context = context;
        }
        

        [HttpGet]
        public IActionResult Index()
        {
            var Personas = _context.tblPersonas.ToList();
            return View(Personas);
        }





        //CREAR
        [HttpGet]
        public IActionResult CrearUsuario()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CrearUsuario(RegistroViewModel Rvm)
        {
            if (Rvm == null) return View();

            

            if (ModelState.IsValid)
            {
                CreatePasswordHash(Rvm.Password, out byte[] PHash, out byte[] Salt);

                Persona P = new Persona()
                {
                    Rut = Rvm.Rut,
                    Nombre = Rvm.Nombre,
                    Apellido = Rvm.Apellido,
                    Cargo = Rvm.Cargo,
                    Direccion = Rvm.Direccion,
                    FechaNacimiento = Rvm.FechaNacimiento,
                    Estado = "Activo",
                    PasswordHash = PHash,
                    PasswordSalt = Salt
                };


                _context.Add(P);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(Rvm);



        }

        private void CreatePasswordHash(string password,
            out byte[] passwordhash, out byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordhash = hmac.ComputeHash(System.Text.Encoding.UTF8
                    .GetBytes(password));
            }
        }




        [HttpGet]
        public IActionResult VerUsuario()
        {
            var Personas = _context.tblPersonas.ToList();
            return View(Personas);
        }



        //EDITAR
        [HttpGet]
        public IActionResult Editar(int IdPersona)
        {
            var P = _context.tblPersonas.FirstOrDefault(m => m.Id == IdPersona);
            if (P == null) return NotFound();
            else
            {
                return View(P);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Editar(Persona P)
        {
            if (ModelState.IsValid)
            {
                _context.Update(P);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(P);
        }


        //ELIMINAR

        [HttpGet]
        public async Task<IActionResult> Eliminar(int IdPersona)
        {
            var P = _context.tblPersonas.Find(IdPersona);
            if (P == null) return NotFound();
            _context.Remove(P);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}



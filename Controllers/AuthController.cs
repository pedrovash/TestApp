using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using TestApp.Models;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    
    public class AuthController : Controller
    {
        

        private readonly AppDbContext _context;
        public AuthController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<ActionResult> LoginIn(string accion )
        {
            
            return View();
        }


        
        [HttpPost]
        public async Task<ActionResult> LoginIn(LoginViewModel lvm)
        {
            var Personas = _context.tblPersonas.ToList();
            Persona? P = new Persona();
            if (Personas.Count == 0)
            {
                P.Nombre = "root";
                P.Cargo = "Administrador";
                P.Rut = "1.111.111-1";
                P.Apellido = "root";
                P.Direccion = "root";
                P.Estado = "root";
                P.FechaNacimiento = "root";
                CreatePasswordHash("root", out byte[] passwordHash, out byte[] passwordSalt);
                P.PasswordHash = passwordHash;
                P.PasswordSalt = passwordSalt;
                _context.Add(P);
                await _context.SaveChangesAsync();
            }


            //LOGIN
            P = null;
            P = _context.tblPersonas.FirstOrDefault(p => p.Rut == lvm.Rut);
            if (P == null)
            {
                ModelState.AddModelError(string.Empty, "Usuario no encontrado");
                return View(lvm);
            }
            else
            {
                if (!VerifyPasswordHash(lvm.Password, P.PasswordHash, P.PasswordSalt))
                {
                    ModelState.AddModelError(string.Empty, "Password incorrecta");
                    return View(lvm);
                }
                else
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, P.Id.ToString()),
                        new Claim(ClaimTypes.Name, P.Rut),
                        new Claim(ClaimTypes.Role, P.Cargo)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                        new AuthenticationProperties { IsPersistent = true });
                    if (P.Cargo.Equals("Administrador"))
                    {
                        return RedirectToAction(nameof(MenuAdmin));
                    }
                    else
                    {
                        return RedirectToAction("MarcarHorario", "AdministrarHorarios" );
                    }
                    
                }

            }
        }

        [Authorize(Roles="Administrador")]
        public IActionResult MenuAdmin()
        {
            return View();
        }


        

        public IActionResult Profile()
        {
            if (User.Identity.IsAuthenticated)
            {
                var Persona = _context.tblPersonas.FirstOrDefault(p => p.Rut == User.Identity.Name);
                ProfileViewModel pvm = new ProfileViewModel()
                {
                    Persona = Persona
                };

                return View(pvm);
            }
            return View();
            //return RedirectToAction(nameof(LoginIn));
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

        private bool VerifyPasswordHash(string password,
            byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8
                    .GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }



    }
    
}


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProvedorArmazenamentoIDMongo.Modelos;

namespace Alura_Challange_Transacao_Financeira.Controllers
{
    [ApiController]
    [Route("Usuario")]
    public class UsuarioController : Controller
    {
        private readonly UserManager<Usuarios> _userManager;
        private readonly SignInManager<Usuarios> _signManager;
        public UsuarioController(UserManager<Usuarios> userManager, SignInManager<Usuarios> signManager)
        {
            _userManager = userManager;
            _signManager = signManager;
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario([FromBody] Usuarios usuario)
        {
            await _userManager.CreateAsync(usuario);
            return Ok("Usuario criado");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Usuarios usuario)
        {
            await _signManager.SignInAsync(usuario, true);
            return View();
        }

    }
}

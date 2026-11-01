using CatalogoJogos.Models;
using CatalogoJogos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoJogos.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public AuthController(ITokenService tokenService) => _tokenService = tokenService;

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request.Usuario != "Mauricio" || request.Senha != "123456")
            return Unauthorized(new { mensagem = "Usuário ou senha inválidos." });

        var token = _tokenService.GerarToken(request.Usuario);
        return Ok(new { token });
    }
}
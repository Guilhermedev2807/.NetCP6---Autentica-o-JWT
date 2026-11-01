using CatalogoJogos.Models;
using CatalogoJogos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoJogos.Controllers;

[ApiController]
[Route("api/jogos")]
[Authorize]
public class JogosController : ControllerBase
{
    private readonly JogoService _service;

    public JogosController(JogoService service) => _service = service;

    [HttpPost]
    public IActionResult Criar([FromBody] Jogo jogo)
    {
        var criado = _service.Criar(jogo);
        return CreatedAtAction(nameof(ObterPorId), new { id = criado.Id }, criado);
    }

    [HttpGet]
    public IActionResult Listar() => Ok(_service.Listar());

    [HttpGet("{id:int}")]
    public IActionResult ObterPorId(int id)
    {
        var jogo = _service.ObterPorId(id);
        return jogo is null ? NotFound() : Ok(jogo);
    }

    [HttpPut("{id:int}")]
    public IActionResult Atualizar(int id, [FromBody] Jogo jogo)
    {
        var atualizado = _service.Atualizar(id, jogo);
        return atualizado is null ? NotFound() : Ok(atualizado);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Remover(int id) =>
        _service.Remover(id) ? NoContent() : NotFound();

    // Desafio LINQ 1
    [HttpGet("busca")]
    public IActionResult Buscar([FromQuery] string plataforma, [FromQuery] decimal precoMaximo) =>
        Ok(_service.Buscar(plataforma, precoMaximo));

    // Desafio LINQ 2
    [HttpGet("relatorio-estoque")]
    public IActionResult RelatorioEstoque() => Ok(_service.RelatorioEstoque());
}
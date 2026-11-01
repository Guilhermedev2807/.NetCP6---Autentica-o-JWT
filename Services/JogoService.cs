using CatalogoJogos.Models;
using CatalogoJogos.Repositories;

namespace CatalogoJogos.Services;

public class JogoService
{
    private readonly IJogoRepository _repository;

    public JogoService(IJogoRepository repository) => _repository = repository;

    public Jogo Criar(Jogo jogo) => _repository.Adicionar(jogo);

    public IEnumerable<Jogo> Listar() => _repository.ObterTodos();

    public Jogo? ObterPorId(int id) => _repository.ObterPorId(id);

    public Jogo? Atualizar(int id, Jogo jogo)
    {
        if (!_repository.Atualizar(id, jogo)) return null;
        return _repository.ObterPorId(id);
    }

    public bool Remover(int id) => _repository.Remover(id);

    // Desafio 1: filtro por plataforma e preço máximo
    public IEnumerable<Jogo> Buscar(string plataforma, decimal precoMaximo) =>
        _repository.ObterTodos()
            .Where(j => j.Plataforma.Equals(plataforma, StringComparison.OrdinalIgnoreCase)
                        && j.Preco <= precoMaximo);

    // Desafio 2: relatório agrupado por plataforma
    public IEnumerable<RelatorioEstoqueDto> RelatorioEstoque() =>
        _repository.ObterTodos()
            .GroupBy(j => j.Plataforma)
            .Select(g => new RelatorioEstoqueDto
            {
                Plataforma = g.Key,
                QuantidadeTitulos = g.Count(),
                ValorTotalInventario = g.Sum(j => j.Preco * j.Estoque)
            });
}
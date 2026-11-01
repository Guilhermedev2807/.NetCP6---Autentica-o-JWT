using CatalogoJogos.Models;

namespace CatalogoJogos.Repositories;

public class JogoRepository : IJogoRepository
{
    private readonly List<Jogo> _jogos = new();
    private int _proximoId = 1;
    private readonly object _lock = new();

    public Jogo Adicionar(Jogo jogo)
    {
        lock (_lock)
        {
            jogo.Id = _proximoId++;
            _jogos.Add(jogo);
            return jogo;
        }
    }

    public IEnumerable<Jogo> ObterTodos()
    {
        lock (_lock) return _jogos.ToList();
    }

    public Jogo? ObterPorId(int id)
    {
        lock (_lock) return _jogos.FirstOrDefault(j => j.Id == id);
    }

    public bool Atualizar(int id, Jogo jogo)
    {
        lock (_lock)
        {
            var existente = _jogos.FirstOrDefault(j => j.Id == id);
            if (existente is null) return false;

            existente.Titulo = jogo.Titulo;
            existente.Plataforma = jogo.Plataforma;
            existente.Genero = jogo.Genero;
            existente.Preco = jogo.Preco;
            existente.AnoLancamento = jogo.AnoLancamento;
            existente.Estoque = jogo.Estoque;
            return true;
        }
    }

    public bool Remover(int id)
    {
        lock (_lock)
        {
            var jogo = _jogos.FirstOrDefault(j => j.Id == id);
            return jogo is not null && _jogos.Remove(jogo);
        }
    }
}
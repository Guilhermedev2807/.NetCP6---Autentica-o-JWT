using CatalogoJogos.Models;

namespace CatalogoJogos.Repositories;

public interface IJogoRepository
{
    Jogo Adicionar(Jogo jogo);
    IEnumerable<Jogo> ObterTodos();
    Jogo? ObterPorId(int id);
    bool Atualizar(int id, Jogo jogo);
    bool Remover(int id);
}
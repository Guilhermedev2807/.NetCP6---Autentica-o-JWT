namespace CatalogoJogos.Services;

public interface ITokenService
{
    string GerarToken(string usuario);
}
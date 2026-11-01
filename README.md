# Catálogo de Jogos - Web API .NET com JWT

Web API em .NET 9 para gerenciamento de um catálogo de jogos de videogame, com dados em memória, CRUD completo, filtros e relatório com LINQ, e autenticação/autorização via **JSON Web Token (JWT)**. A documentação interativa é feita com **OpenAPI + Scalar**.

## Integrantes

| Nome | RM |
|------|----|
| Guilherme de Andrade Martini | RM566087 |
| Raphael Gomes Mancera | RM562279 |
| Bruno Vinicius Barbosa | RM566366 |

## Tecnologias

- .NET 9 (ASP.NET Core Web API com Controllers)
- Autenticação JWT Bearer (`Microsoft.AspNetCore.Authentication.JwtBearer`)
- OpenAPI (`Microsoft.AspNetCore.OpenApi`)
- Scalar (`Scalar.AspNetCore`)
- LINQ

## Arquitetura

Separação em camadas com injeção de dependência nativa do .NET:

```
Controller  >  JogoService  >  IJogoRepository
```

```
CatalogoJogos/
├── Controllers/    AuthController, JogosController
├── Models/         Jogo, LoginRequest, RelatorioEstoqueDto
├── Repositories/   IJogoRepository, JogoRepository (em memória, Singleton)
├── Services/       JogoService, ITokenService, TokenService
├── Security/       BearerSecuritySchemeTransformer (esquema Bearer no Scalar)
├── appsettings.json
└── Program.cs
```

## Como executar

Pré-requisito: [.NET SDK 9](https://dotnet.microsoft.com/download).

```bash
cd CatalogoJogos
dotnet run
```

Depois acesse a documentação em:

```
http://localhost:5049/scalar/v1
```

> A porta pode variar. Confira a mensagem `Now listening on:` no terminal.

## Configuração do JWT

As configurações ficam no `appsettings.json`:

```json
"Jwt": {
  "Key": "chave-super-secreta-com-pelo-menos-32-caracteres-123456",
  "Issuer": "CatalogoJogosApi",
  "Audience": "CatalogoJogosClientes"
}
```

## Como autenticar e testar

1. Faça `POST /api/auth/login` com:
   ```json
   { "usuario": "Mauricio", "senha": "123456" }
   ```
2. Copie o valor de `token` da resposta.
3. No Scalar, abra **Authentication**, selecione **Bearer** e cole o token.
4. Teste as rotas protegidas. Sem token, elas retornam **401 Unauthorized**.

## Endpoints

### Autenticação (rota aberta)

| Método | Rota | Descrição | Retorno |
|--------|------|-----------|---------|
| POST | `/api/auth/login` | Gera o token JWT | 200 / 401 |

### Jogos (protegidas com `[Authorize]`)

| Método | Rota | Descrição | Retorno |
|--------|------|-----------|---------|
| POST | `/api/jogos` | Cadastra um jogo | 201 |
| GET | `/api/jogos` | Lista todos os jogos | 200 |
| GET | `/api/jogos/{id}` | Busca jogo por ID | 200 / 404 |
| PUT | `/api/jogos/{id}` | Atualiza um jogo | 200 / 404 |
| DELETE | `/api/jogos/{id}` | Remove um jogo | 204 / 404 |
| GET | `/api/jogos/busca?plataforma={p}&precoMaximo={v}` | Filtra por plataforma e preço máximo | 200 |
| GET | `/api/jogos/relatorio-estoque` | Relatório de estoque agrupado por plataforma | 200 |

## Exemplo de jogo

```json
{
  "titulo": "Gran Turismo 7",
  "plataforma": "PlayStation 5",
  "genero": "Corrida",
  "preco": 249.90,
  "anoLancamento": 2022,
  "estoque": 10
}
```

## Observações

- Os dados são armazenados **em memória**: ao reiniciar a aplicação, a lista de jogos é zerada.
- O repositório é registrado como **Singleton** para manter a lista durante a execução.
- O token expira em 1 hora.
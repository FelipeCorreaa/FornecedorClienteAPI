# Projeto APIClienteFornecedor

Este é um projeto de exemplo que visa demonstrar a implementação de uma API para gerenciamento de produtos, fornecedores e pedidos, utilizando ASP.NET Core, Entity Framework Core e PostgreSQL. A API inclui também autenticação JWT (JSON Web Token) para proteger as operações sensíveis.


## Estrutura do Projeto.
aonde temos relacionamento entre :
produto e fornecedor
cliente e pedidos
ordem de pedido com cliente e fornecedor


na base do projeto temos um arquivo texto Criartabelas.txt para criar as tabelas e relacionamentos.

### 1. Pasta `APIClienteFornecedor`

A pasta raiz do projeto contém os arquivos principais da aplicação.

#### 1.1 `Program.cs` e `Startup.cs`

- **Program.cs**: O ponto de entrada da aplicação, responsável por criar e configurar o host da web.
- **Startup.cs**: Configuração do serviço, injeção de dependência e configuração do pipeline de solicitação.

#### 1.2 `Controllers`

A pasta contém os controladores da API.

- **`ProdutoController.cs`**: Controlador responsável pelas operações relacionadas aos produtos, como criação, atualização, exclusão e consulta.

#### 1.3 `Handlers`

Os handlers representam a camada de manipulação de comandos.

- **`CriarProdutoCommandHandler.cs`**: Manipulador para criar um novo produto.
- **`AtualizarProdutoCommandHandler.cs`**: Manipulador para atualizar as informações de um produto.
- **`DeleteProdutoCommandHandler.cs`**: Manipulador para excluir um produto.

#### 1.4 `Queries`

Contém as classes responsáveis pela execução de consultas.

- **`ProdutoQueryParameters.cs`**: Parâmetros de consulta para a busca de produtos.

#### 1.5 `Repository`

Contém a implementação da camada de repositório.

- **`ProdutoRepository.cs`**: Repositório para operações relacionadas aos produtos, como consulta e manipulação.

#### 1.6 `Services`

- **`TokenService.cs`**: Serviço responsável pela geração e validação de tokens JWT.

#### 1.7 `Data`

Contém a configuração do DbContext e as entidades.

- **`ApplicationDbContext.cs`**: Configuração do contexto do banco de dados.

#### 1.8 `Commands`

Contém as classes de comandos.

- **`CriarProdutoCommand.cs`**: Comando para criar um novo produto.
- **`AtualizarProdutoCommand.cs`**: Comando para atualizar informações de um produto.

#### 1.9 `Models`

Contém as entidades do sistema.

- **`Produto.cs`**: Entidade representando um produto.

### 2. Autenticação JWT

A autenticação JWT é implementada para proteger as operações sensíveis da API. O processo é o seguinte:

- **Geração do Token**: Antes de realizar qualquer operação protegida, é necessário gerar um token JWT utilizando a Access Key ID e Access Key Secret fornecidas.

Exemplo de geração de token usando cURL:

```bash
curl --location --request POST 'https://sua-api.com/token' \
--header 'Content-Type: application/json' \
--data-raw '{
    "AccessKeyId": "Acces1234",
    "AccessKeySecret": "Acces1234"
}'

### EXEMPLOS para inserção :
### 1. Criar Produto

curl --location --request POST 'https://localhost:5001/Produto' \
--header 'Authorization: Bearer SeuTokenJWTAqui' \
--header 'Content-Type: application/json' \
--data-raw '{
    "Nome": "NovoProduto",
    "Preco": 19.99,
    "SupplierId": 1
}'

### 2. Atualizar Produto
curl --location --request PUT 'https://localhost:5001/Produto/NomeDoProduto' \
--header 'Authorization: Bearer SeuTokenJWTAqui' \
--header 'Content-Type: application/json' \
--data-raw '{
    "Preco": 29.99,
    "SupplierId": 2
}'



curl --location --request DELETE 'https://localhost:5001/Produto/NomeDoProduto' \
--header 'Authorization: Bearer SeuTokenJWTAqui'


curl --location --request GET 'https://localhost:5001/Produto?pagina=1&tamanhoPagina=10' \
--header 'Authorization: Bearer SeuTokenJWTAqui'




🛒 API E-Commerce RESTful

**Importante: temos um usuario ja criado para você acessar com autenticação professor use o Professor@gmail.com e pegue o token e cole no authorize do swagger

Integrantes:
Jean Roberto Gomes, RM94418
Giovanna Laturague Bueno, RM556242
Esta é uma API RESTful desenvolvida em C# .NET 6 que simula o backend de um sistema de e-commerce. O sistema gerencia usuários, produtos e pedidos, 
incluindo o relacionamento N:N entre pedidos e produtos. A API utiliza Web API, facilitando a manutenção, extensão e integração com front-end.

🛠 Tecnologias Utilizadas
C# .NET 8 com ASP.NET Core Web API
Entity Framework Core (Code First + Migrations)
Swagger / OpenAPI (documentação interativa)
SQL Server (ou outro banco configurado no dbContext)
DTOs (Data Transfer Objects) para organização e segurança dos dados
Repository Pattern para separação de responsabilidades
HATEOAS para respostas enriquecidas com links de navegação
JWT (JSON Web Token) para autenticação e autorização
Health Checks para monitoramento da API
Testes Unitários com xUnit


🚀 Funcionalidades
👤 Usuários
Criar, listar, atualizar e deletar usuários
Acesso protegido via JWT

📦 Produtos
Criar, listar, atualizar e deletar produtos

📝 Pedidos
Criar pedidos vinculados a usuários e produtos
Calcular automaticamente o valor total do pedido
Listar pedidos com seus respectivos produtos
Atualizar ou deletar pedidos

🔄 Relacionamento N:N
Implementado via tabela intermediária PedidoProduto

💻 Monitoramento
Endpoint /health para Health Checks da API

🔐 Segurança
Autenticação e autorização via JWT
Mensagens customizadas:
401 Unauthorized → usuário não autenticado

⚡ Como Executar o Projeto

Clone o repositório:
git clone 

Execute a API:
dotnet run

Verifique o Health Check: https://localhost:<porta>/health

Garantem integridade de:

Criação de usuários e produtos

Cálculo de pedidos

Validações de JWT

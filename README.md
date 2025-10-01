Nome dos integrantes:Jean Roberto Gomes, RM94418; Giovanna Laturague Bueno, RM556242

Este projeto é uma API RESTful desenvolvida em C# .NET 6 que simula o backend de um sistema de e-commerce.
O sistema gerencia usuários, produtos e pedidos, incluindo o relacionamento N:N (muitos-para-muitos) entre pedidos e produtos. 
Optamos por utilizar uma WEBAPI pela organização que ela proporcina e fica mais facil adicionar coisas novas.

 Tecnologias Utilizadas

C# .NET 6
ASP.NET Core Web API
Entity Framework Core (Code First + Migrations)
Swagger / OpenAPI (documentação interativa)
SQL Server (ou outro banco configurado no dbContext)
DTOs (Data Transfer Objects) para segurança e organização dos dados
Repository Pattern para separação de responsabilidades
HATEOAS para enriquecer as respostas com links de navegação

 Funcionalidades

Usuários
Criar, listar, atualizar e deletar usuários

Produtos
Criar, listar, atualizar e deletar produtos

Pedidos
Criar pedidos vinculados a usuários e produtos
Calcular automaticamente o valor total do pedido
Listar pedidos com seus respectivos produtos
Atualizar ou deletar pedidos

Relacionamento N:N
Implementado via tabela intermediária PedidoProduto

 Como Executar o Projeto?

Exemplos de payloas para os metodos post e updates (Estão disponivel os payloads estão dentros dos metodos nos swagger), metodos gets apenas executar

Pedido -
{
  "usuarioId": 0,
  "produtos": [
    {
      "produtoId": 0,
      "quantidade": 0
    }
  ]
}

Produto -
{
  "nome": "string",
  "preco": 0
}

Usuario -
{
  "nome": "string",
  "email": "user@example.com"
}

Metodos updates abaixo 

Usuario - 
{
  "nome": "string",
  "email": "user@example.com"
}

Produto - 
{
  "nome": "string",
  "preco": 0
}

Pedido -
{
  "valorTotal": 0
}

# Projeto de demonstração - Portfólio
Projeto feito para ser utilizado como portfólio. O sistema contará com recurso de autenticação JWT , implementação de desenvolvimento Low Code utilizando o SQL Server e EntityFrameworkCore.

## Desenvolvimento
- API em .NET 9
- Autenticação com JWT
- SQL Server
- Padrão de desenvolvimento Low Code
- Conceitos aplicados de Clean Code
- Tradução utilizando MongoDb e Redis

## Recursos
- Sistema de autenticação JWT
- Cadastro de Usuários
- Cadastro de Clientes
- Cadastro de Produtos e categorização

### Planejamento - **Em desenvolvimento**
A ideia é desenvolver um mini-ERP contando com alguns recursos simples apenas para demonstração de funcionalidade
- Cadastros (Cliente / Fornecedor / Marca / Produto)
- Gestão de estoque
- Gestão de movimentação financeira
- Envio de e-mail
- Suporte a multi idiomas

## Requisitos
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- [.NET SDK 9.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Redis] (https://redis.io/downloads/) (opcional)
- [MongoDB] (https://www.mongodb.com/try/download/community)

# Como executar
- Importar os dados do arquivo resources/dump/Translate.json no MongoDB

- Altere o arquivo appsettings.josn para alterar a ConnectionString
- Caso não deseje utilizar o redis, a flag UseRedis no appsettings.json deverá ser alterada para false.

- No promp de comando, acesse a pasta da API 
```bash
Shoply\src\Shoply.Api
```
- Atualize o banco de dados executando as migrations
```bash
dotnet ef database update
```
- Execute o Projeto
```bash
dotnet run
```
- Acessar a API
```bash
http://localhost:5074
```

## Estrutura

A estrutura do projeto segue um padrão de clean code. A Solution foi segmentada em vários projetos para garantir legibilidade e modularidade aos componentes.

Shoply.Api - Onde ficam disponibilizados todos os endpoints da aplicação
Shoply.Application - Onde ficam disponilizados serviços auxiliares, como autenticação, envio de e-mail, entre outros. 
Shoply.Arguments - Contém todas as classes comuns no sistema e onde ficam todos os objetos de entrada/saída da API. Inputs de criação, atualização e os Outputs.
Shoply.Domain - Toda a parte de regra de negócio do sistema
Shoply.Infrastructure - Toda a parte de persistência no banco de dados relacional (SQL Server). Repositório, entidades, unidade de trabalho, Fluent Mapping e outros
Shoply.Security - Contém toda a parte de criptografia e outros recursos de segurança do projeto
Shoply.Translation - Destinado a toda a parte de tradução do sistema, utilizando como base o MongoDB e o Redis para agilizar o processo de tradução

Shoply.CodeGenerator - Esse não faz parte da estrutura, é um console App destinado a geração de todas as camadas para a criação de uma nova entidade. Ele é responsável pela escrita em todos os outros projetos citados acima. Ainda está em desenvolvimento.

Todos os projetos foram feitos em .NET 9.
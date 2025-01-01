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
- Limite de 1 acesso por usuário, impedindo que mais de uma pessoa utilize o mesmo usuário simultâneamente. 

### Recurso de consulta dinâmica
- O sistema conta com suporte a queries dinâmicas para consulta, permitindo que o usuário escolha quais as propriedades ele queira no retorno a partir do objeto pré configurado. Dando mais liberdade para o usuário e reduzindo a necessidade de manutenções esporádicas para  customizações por parte do desenvolvimento. O recurso já está disponível para todos os endpoints de consulta.
- A consulta dinâmica é feita através do Header RETURN-PROPERTY, que recebe apenas um array com os nomes das propriedades, internamente é interpretada para gerar uma consulta no EntityFrameworkCore e montar o objeto com o resultado.
- Não possui limite de profundidade para as consultas
#### Exemplo de utilização da consulta dinâmica:

```
["Id", "Code", "FirstName", "LastName", "BirthDate", "Document", "PersonType", "ListCustomerAddress.Id","ListCustomerAddress.CustomerId", "ListCustomerAddress.AddressType", "ListCustomerAddress.PublicPlace", "ListCustomerAddress.Number", "ListCustomerAddress.Complement", "ListCustomerAddress.Neighborhood", "ListCustomerAddress.PostalCode", "ListCustomerAddress.Reference", "ListCustomerAddress.Observation"]
```
#### Retorno da API:
```
{
  "result": {
    "Id": 1,
    "Code": "001",
    "FirstName": "Christian",
    "LastName": "Ribeiro",
    "BirthDate": "2025-01-01",
    "Document": "41363206044",
    "PersonType": 0,
    "ListCustomerAddress": [
      {
        "Id": 5,
        "CustomerId": 1,
        "AddressType": 0,
        "PublicPlace": "Rua Rio Azul",
        "Number": "12345",
        "Complement": "Casa",
        "Neighborhood": "Emaús",
        "PostalCode": "59148743",
        "Reference": "Posto do seu zé",
        "Observation": "Apenas horário comercial"
      }
    ]
  }
}
```

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
- [Redis](https://redis.io/downloads/) (Opcional - desativar FeatureFlag no appsettings.json)
- [MongoDB](https://www.mongodb.com/try/download/community)

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
- A autenticação no sistema é feita através do endpoint */api/v1/User/Authenticate* com o seguinte usuário padrão:
```
{
  "email": "default@shoply.com",
  "password": "string"
}
```
## Estrutura

A estrutura do projeto segue um padrão de clean code. A Solution foi segmentada em vários projetos para garantir legibilidade e modularidade aos componentes.

- **Shoply.Api** - Onde ficam disponibilizados todos os endpoints da aplicação
- **Shoply.Application** - Onde ficam disponilizados serviços auxiliares, como autenticação, envio de e-mail, entre outros. 
- **Shoply.Arguments** - Contém todas as classes comuns no sistema e onde ficam todos os objetos de entrada/saída da API. Inputs de criação, atualização e os Outputs.
- **Shoply.Domain** - Toda a parte de regra de negócio do sistema
- **Shoply.Infrastructure** - Toda a parte de persistência no banco de dados relacional (SQL Server). Repositório, entidades, unidade de trabalho, Fluent Mapping e outros
- **Shoply.Security** - Contém toda a parte de criptografia e outros recursos de segurança do projeto
- **Shoply.Translation** - Destinado a toda a parte de tradução do sistema, utilizando como base o MongoDB e o Redis para agilizar o processo de tradução

**Shoply.CodeGenerator** - Esse não faz parte da estrutura, é um console App destinado a geração de todas as camadas para a criação de uma nova entidade. Ele é responsável pela escrita em todos os outros projetos citados acima. Ainda está em desenvolvimento.

Todos os projetos foram feitos em .NET 9.

# 🎯 Fluxo Caixa
Serviço para gerenciamento de fluxo de caixa. Com ele, é possível cadastrar lançamentos de crédito e débito e consultar o saldo diário.

## 🔨 Desenho da arquitetura
![alt text](https://github.com/thomasmoreira/CashFlow/blob/master/Arquitetura.jpg?raw=true)

## Padrões de microserviço utilizados
* Separação em camadas
* Injeção de dependências
* DTOs
  
## Padrões de projetos utilizados

- `Repository`: utilizado para abstrair a camada de acesso ao banco de dados.
- `Generic Repository`: Utlizado para permitir o reuso de recursos para acesso ao banco dados.
- `Unit Of Work`: Utlizado para permitir o reuso de recursos para acesso ao banco dados.
- `DTO`: utilizado para transferência de dados entre as camadas.


## ✔️ Tecnologias, recursos e bibliotecas usadas
- ``Aspnet Core 6``
- ``EF Core 7``
- ``MySql``
- ``Serilog``
- ``Seq``
- ``Fluent Validation``
- ``JWT``
- ``Swagger 3``
- ``NUnit``

## 🛠️ Execução do projeto
Para executar o projeto, é necessário ter o Docker e o Docker Compose instalados.

## 🚀 Como usar
## Clone o repositório:

```
git clone https://github.com/thomasmoreira/CashFlow.git
```
## 📁 Entre na pasta do projeto:
```
cd CashFlow
```
## 🐳 Execute o docker-compose:
```bash
docker-compose up
```
O serviço de lançamentos estará disponível em http://localhost:8000/swagger.
O serviço de relatórios estará disponível em http://localhost:8081/swagger.
A interface do Seq estará disponível em http://localhost:5341

## 🔑 Autenticação por token
Para utilizar as funcionalidades da API, é necessário realizar a autenticação e obter um token JWT.

```
username: admin
password: Pass@word123!

username: user
password: 123456
```

A resposta será um token JWT, que deve ser incluído no header das requisições que exigem autenticação, no formato "Bearer {token}"

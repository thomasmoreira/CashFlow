# 🎯 API Fluxo Caixa
Serviço para gerenciamento de fluxo de caixa. Com ele, é possível cadastrar lançamentos de crédito e débito e consultar o saldo diário.

## 🔨 Desenho da arquitetura
![Capturar](https://github.com/thomasmoreira/CashFlow/assets/109549155/26a27cc6-29fa-4569-9d94-1e3ebdc3b8d7)

## Padrões de microserviço utilizados
* Separação em camadas
* Injeção de dependências
* DTOs
  
## Padrões de projetos utilizados

- `Repository`: utilizado para abstrair a camada de acesso ao banco de dados.
- `Generic Repository`: Utlizado para permitir o reuso de recursos para acesso ao banco dados.
- `Unit Of Work`: Utlizado para permitir o reuso de recursos para acesso ao banco dados.
- `DTO`: utilizado para transferência de dados entre as camadas.


## ✔️ Tecnologias e bibliotecas usadas
- ``Aspnet Core 6``
- ``EF Core 7``
- ``Serilog``
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

A resposta será um token JWT, que deve ser incluído no header das requisições que exigem autenticação, no formato "Bearer {token}".
## Testes unitários
Para rodar os testes unitários, execute o comando abaixo:

```
mvn test
```
![testes unitarios](https://github.com/pauloruszel/controle-fluxo-caixa/assets/12766450/30cddadd-240e-4fe1-a2ac-d14a4f6af84d)

## Observability
A aplicação possui o Spring Boot Actuator configurado para expor os endpoints /health, /info e /metrics na porta 1979. 
Para acessá-los, utilize o seguinte endereço: 
* http://localhost:1979/actuator/health.

# 🎯 API Fluxo Caixa
Serviço para gerenciamento de fluxo de caixa. Com ele, é possível cadastrar lançamentos de crédito e débito e consultar o saldo diário.

## 🔨 Desenho da arquitetura
![api-fluxo-caixa drawio](https://user-images.githubusercontent.com/12766450/236968499-3a6b8f77-1b4e-43ab-bc54-f95e3e5b837c.png)

## Padrões de microserviço utilizados
* Separação em camadas
* Injeção de dependências
* DTOs
  
## Padrões de projetos utilizados

- `Repository`: utilizado para abstrair a camada de acesso ao banco de dados.
- `Generic Repository`: Utlizado para permitir o reuso de recursos para acesso ao banco dados.
- `DTO`: utilizado para transferência de dados entre as camadas.


## ✔️ Tecnologias e bibliotecas usadas
- ``Java 17``
- ``Spring Boot 3``
- ``Spring Data JPA``
- ``Spring Security``
- ``Spring Boot Actuator``
- ``JWT``
- ``H2 Database``
- ``Swagger 3``
- ``Lombok``
- ``JUnit 5``
- ``Mockito``
- ``ModelMapper``

## 🛠️ Execução do projeto
Para executar o projeto, é necessário ter o Docker e o Docker Compose instalados.

## 🚀 Como usar
## Clone o repositório:

```
git clone https://github.com/pauloruszel/controle-fluxo-caixa.git
```
## 📁 Entre na pasta do projeto:
```
cd controle-fluxo-caixa
```
### Execute o comando abaixo para compilar e empacotar o projeto:
```bash
mvn clean package
```
## 🐳 Execute o docker-compose:
```bash
docker-compose up --build
```
A API estará disponível em http://localhost:8080.

## 🔑 Autenticação por token
Para utilizar as funcionalidades da API, é necessário realizar a autenticação e obter um token JWT.

Endpoint de autenticação:
POST /login
```
Body:
{
    "login": "Paulo",
    "password": "1234"
}
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

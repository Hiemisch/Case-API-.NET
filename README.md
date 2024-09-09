# API .NET Cliente e Transferência

## Descrição

Esta é uma API RESTful desenvolvida em .NET 8, destinada a gerenciar operações bancárias, incluindo o cadastro de clientes e transferências entre contas. A aplicação utiliza um banco de dados in-memory e inclui funcionalidades para listagem e cadastro de clientes, realização e listagem de transferências e testes unitários e de integração.

## Funcionalidades

- *Cadastro de Clientes*: Permite adicionar novos clientes ao sistema.
- *Transferência entre Contas*: Facilita a movimentação de valores entre contas.
- *Listagem de Clientes*: Consulta e exibe informações sobre clientes cadastrados.
- *Listagem de Clientes(Por número de conta)*: Consulta e exibe informações sobre clientes cadastrados,dependendo da conta pesquisada.
- *Listagem de Transferências*: Consulta e exibe informações sobre as transferências do número de conta fornecido.

## Tecnologias

- *.NET 8*
- *Banco de Dados In-Memory*
- *Swagger*: Para documentação da API.
- *Testes Unitários e de Integração*

## Endpoints

### Cliente

- *POST /clientes*: Cadastra um novo cliente.
- *GET /clientes*: Lista todos os clientes.
- *GET /clientes/{numeroConta}*: Obtém detalhes de um cliente específico.

### Transferência

- *POST /transferencias*: Realiza uma transferência entre contas.
- *GET /transferencias/{numeroConta}*: Retorna todas as transferências associadas a um determinado número de conta.

## Configuração

1. Clone o repositório.
2. Execute o projeto em .NET 8.
3. Utilize o Swagger para testar a API.

## Versão

1.0

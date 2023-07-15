# Projeto POC: Integração de Tecnologias com .NET Core 7

# Descrição do Projeto
- Este repositório contém o código-fonte de uma prova de conceito (POC) que visa demonstrar a integração de várias tecnologias utilizando o framework .NET Core 7. O objetivo principal é explorar a comunicação assíncrona entre sistemas através do uso de Kafka, bem como a utilização de diferentes bancos de dados, incluindo SQL Server, Redis, RabbitMQ e MongoDB.
---
# Tecnologias Utilizadas
- .NET Core 7: Framework para desenvolvimento da Microsoft.
- AutoMapper: Biblioteca para realizar mapeamento entre objetos.
- Swagger: Documentação para a API.
- DDD: Domain Drive Design modelagem de desenvolvimento orientado a injeção de dependência.
- Entity FrameWork
- Dapper
- XUnit
- FluentValidator
- Azure.Identity
- MongoDb
- Redis
- Serilog
- Health check
- RabbitMQ
- Kafka
---

# Funcionalidades
- A POC possui as seguintes funcionalidades principais:

# Comunicação Assíncrona com Kafka:
- Demonstração da publicação e consumo de mensagens em tópicos do Kafka.
- Implementação de produtores e consumidores de mensagens utilizando a biblioteca do Kafka para .NET.

# Persistência de Dados:
- Utilização do SQL Server para armazenamento de dados relacionais.
- Uso do Redis como cache para melhorar o desempenho das consultas.
- Integração com o RabbitMQ para processamento de mensagens assíncronas.
- Utilização do MongoDB para armazenamento de dados não relacionais.
- Configuração do Ambiente de Desenvolvimento

# Para configurar o ambiente de desenvolvimento e executar este projeto, siga as etapas abaixo:

- Instale o .NET Core 7: https://dotnet.microsoft.com/download

# Configuração do Kafka:

- Instale e configure um cluster do Kafka.
- Atualize as configurações de conexão do Kafka no arquivo de configuração do projeto.
- Configuração dos Bancos de Dados:

- Instale e configure o SQL Server, Redis, RabbitMQ e MongoDB em seu ambiente.
- Aalize as strings de conexão nos arquivos de configuração correspondentes.
- Clone este repositório para o seu ambiente local.

# Health Checks
- https://localhost:7102/monitor#/healthchecks
- https://libraries.io/nuget/AspNetCore.HealthChecks.Rabbitmq

# Projeto 
- http://localhost:5072/index.html
- http://localhost:5072/swagger/index.html

# Docker
- http://localhost:5072/swagger/index.html

# RabbitMQ
- http://localhost:15672

# SQLServer
- Server=sqlserver;Integrated Security=true;Initial Catalog=Demo12072023;User Id=sa;Password=@C23l10a1985;Trusted_Connection=false;MultipleActiveResultSets=true;Encrypt=True;TrustServerCertificate=True;

# Docker
- docker-compose up

# SQL Server - Migration
- Update-Database -Context  SQLServerContext


## Autor

- Guilherme Figueiras Maurila

[![Linkedin Badge](https://img.shields.io/badge/-Guilherme_Figueiras_Maurila-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/guilherme-maurila-58250026/)](https://www.linkedin.com/in/guilherme-maurila-58250026/)
[![Gmail Badge](https://img.shields.io/badge/-gfmaurila@gmail.com-c14438?style=flat-square&logo=Gmail&logoColor=white&link=mailto:gfmaurila@gmail.com)](mailto:gfmaurila@gmail.com)
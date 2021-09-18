# Notes

What are the steps for building a microservice architectured project?
Solution Architecture
- Service contexts with resources
- Communication between services
- External communication
Project Architecture
Domain Bounded Contexts
K8S Architecture

What are the steps for building an API?
* Create new project
* Delete default controller and model
* Add library references:
    * dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
    * dotnet add package Microsoft.EntityFrameworkCore
    * dotnet add package Microsoft.EntityFrameworkCore.Design
    * dotnet add package Microsoft.EntityFrameworkCore.InMemory
    * dotnet add package Microsoft.EntityFrameworkCore.SqlServer
* Data Layer:
    * Models
    * DbContext
    * Repository
    * DTOs
    * DI DbContext with InMemory DB for dev and MSSQL for prod
* 

What are the steps for building a Domain Project?
* Create new project
    * dotnet new classlib -o MyEcommerce.Core

## CLI

* Dotnet:
    * dotnet new webapi -n [Project Name]
    * dotnet new classlib -n [Project Name]
    * dotnet add package [Package Name]
* Visual Studio Code
    * code -r .

## TODOS

* Create API for creating and retrieving products
* Seed product db
* Test in postman
* Create docker image
* Deploy to k8s
    * Access via api gateway (ingress-nginx)
* Create webapp
    * Pull in products
    * Basic nav and filters for a catalog

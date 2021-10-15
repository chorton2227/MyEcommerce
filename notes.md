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

- Create new project
- Delete default controller and model
- Add library references:
  - dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
  - dotnet add package Microsoft.EntityFrameworkCore
  - dotnet add package Microsoft.EntityFrameworkCore.Design
  - dotnet add package Microsoft.EntityFrameworkCore.InMemory
  - dotnet add package Microsoft.EntityFrameworkCore.SqlServer
- Data Layer:
  - Models
  - DbContext
  - Repository
  - DTOs
  - DI DbContext with InMemory DB for dev and MSSQL for prod
-

What are the steps for building a Domain Project?

- Create new project
  - dotnet new classlib -o MyEcommerce.Core

How do you handle TLS?

- Generate self-sign cert:
  - openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout ${KEY_FILE} -out ${CERT_FILE} -subj "/CN=${HOST}/O=${HOST}"
- Store as secret:
  - kubectl create secret tls ${CERT_NAME} --key ${KEY_FILE} --cert ${CERT_FILE}
- Reference secret in ingress controller

Ecommerce site:

- Hero image
- Products:
  - List
  - Filter
- Shopping Cart:
  - Add to cart
  - List cart
- Order:
  - Checkout
  - Order
  - Emails
- Payment:
  - Approve checkout -> order creation

## CLI

- Dotnet:
  - dotnet new webapi -n [Project Name]
  - dotnet new classlib -n [Project Name]
  - dotnet add package [Package Name]
- Visual Studio Code
  - code -r .
- Secrets:
  - kubectl create secret generic mssql --from-literal=SA_PASSWORD="P@ssw0rd" -n myecommerce
- MSSQL
  - Create database:
    - Login: /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P [Enter Password]

## TODOS

- Sort by?
  - Default
  - Price
- Return count, page, and size
- Browse categories
- Filter tags and price
- New products
- On sale products

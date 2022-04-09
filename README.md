# Product Order System
Demo system using .NET Minimal API microservices with Mass Transit and Supabase backend services.

- .NET 6
- Docker
- RabbitMQ
- Supabase

Note: This solution/project was created with Jetbrains Rider



## Installing Open API Tooling

```
dotnet new tool-manifest
dotnet tool install SwashBuckle.AspNetCore.Cli
```

## Supabase Configuration

For `CustomerService.Api`, `OrderService.Api` and `ProductService.Api`

```shell
dotnet user-secrets set "Supabase:Url" "SUBABASE URL"
dotnet user-secrets set "Supabase:Key" "SUBABASE URL"
```

Postgres Connection

For `InventoryService.Api`


```shell
dotnet user-secrets set "ConnectionStrings:Postgres" "SUBABASE POSTGRES CONNECTION STRING"
```

## Running docker services

Used for running RabbitMQ

```shell
docker compose up
```

### Customer Service API: 
https://localhost:7080/swagger/index.html

### Product Service API: 
https://localhost:7267/swagger/index.html

### Order Service API: 
https://localhost:7005/swagger/index.html

### Inventory Service API:
https://localhost:7282/swagger/index.html
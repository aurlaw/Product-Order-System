# Product Order System
Demo system using .NET Minimal API microservices with Mass Transit and Supabase backend services.

- .NET 6
- Docker

Note: This solution/project was created with Jetbrains Rider



## Installing Open API Tooling

```
dotnet new tool-manifest
dotnet tool install SwashBuckle.AspNetCore.Cli
```

## Running docker services

```shell
docker compose up
```

### Customer Service API: 
https://localhost:7080/swagger/index.html

### Product Service API: 
https://localhost:7267/swagger/index.html

### Order Service API: 
https://localhost:7005/swagger/index.html
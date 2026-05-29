# Renting Microservice — Prueba Técnica

Microservicio de gestión de flota de vehículos de renting, implementado sobre el template de arquitectura hexagonal de GtMotive.

## Requisitos previos

- [.NET SDK 9.0.200](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/products/docker-desktop/) (para MongoDB)

## Ejecución local

```bash
# 1. Levantar MongoDB
docker-compose up -d

# 2. Ejecutar la API
dotnet run --project src/GtMotive.Estimate.Microservice.Host

# 3. Abrir Swagger
# https://localhost:5001/swagger
```

## Ejecutar tests

```bash
# Todos los tests (15 total)
dotnet test src/microservice.sln

# Solo unitarios (13)
dotnet test test/unit/GtMotive.Estimate.Microservice.UnitTests

# Solo funcionales (1)
dotnet test test/functional/GtMotive.Estimate.Microservice.FunctionalTests

# Solo infraestructura (1)
dotnet test test/infrastructure/GtMotive.Estimate.Microservice.InfrastructureTests
```

> **Nota:** El test funcional requiere MongoDB corriendo en `localhost:27017`.
> El test de infraestructura NO requiere MongoDB (solo valida el pipeline HTTP).

## Casos de uso implementados

| # | Caso de Uso | Endpoint | Descripción |
|---|-------------|----------|-------------|
| 1 | **Crear Vehículo** | `POST /api/vehicles` | Alta de vehículo en la flota. Valida que la fecha de fabricación no supere los 5 años. |
| 2 | **Listar Disponibles** | `GET /api/vehicles/available` | Lista todos los vehículos disponibles para alquilar. |
| 3 | **Alquilar Vehículo** | `POST /api/vehicles/{id}/rent` | Alquila un vehículo a un renter. Valida que el renter no tenga ya un alquiler activo. |
| 4 | **Devolver Vehículo** | `POST /api/vehicles/{id}/return` | Devuelve un vehículo alquilado, marcándolo como disponible de nuevo. |

## Reglas de negocio

1. **Antigüedad máxima:** No se admiten vehículos con fecha de fabricación superior a 5 años → validado en el Value Object `ManufactureDate`.
2. **Un alquiler activo por renter:** Un mismo renter no puede tener más de un vehículo alquilado simultáneamente → validado en `RentVehicleUseCase` consultando `IRentalRepository.HasActiveRental()`.

## Estructura del repositorio

```
src/
├── GtMotive.Estimate.Microservice.Domain/          # Entidades, VOs, Ports, Excepciones
├── GtMotive.Estimate.Microservice.ApplicationCore/  # Use Cases (Input/Output/OutputPort)
├── GtMotive.Estimate.Microservice.Api/              # Controller, Handlers, Presenters
├── GtMotive.Estimate.Microservice.Infrastructure/   # MongoDB Repositories, MongoService
├── GtMotive.Estimate.Microservice.Host/             # Composition Root (Program.cs)
└── microservice.sln

test/
├── unit/           # 13 tests unitarios (Moq)
├── functional/     # 1 test funcional (MediatR + MongoDB real)
├── infrastructure/ # 1 test de infraestructura (TestServer HTTP)
└── load/           # JMeter (no implementado)
```

# Architecture

This backend uses Clean Architecture with four layers:
- Domain: entities, enums, value objects, interfaces, exceptions
- Application: DTOs, service interfaces, use-cases, mapping
- Infrastructure: EF Core persistence, repositories, deduplication implementation
- API: controllers, middleware, filters, app startup

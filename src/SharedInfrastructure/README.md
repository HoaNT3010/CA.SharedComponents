# CA.SharedComponents.SharedInfrastructure

Provides persistence and technical implementations that support the Domain and Application layers.

This project contains EF Core implementations, repository and unit of work patterns.

## Purpose

To reduce boilerplate when implementing:

- Repository pattern
- Unit of Work pattern

## Dependencies

- CA.SharedComponents.Domain
- CA.SharedComponents.Application
- Ardalis.Specification.EntityFrameworkCore
- Entity Framework Core

## What’s Included

| Component                  | Description                                           |
| -------------------------- | ----------------------------------------------------- |
| Repository implementations | Generic repository with specification pattern support |
| EFUnitOfWork               | Transaction management with EF Core DbContext         |

## Important Notes

This project is optional.
Only use it if your service follows the same persistence approach.

## Design Principles

- Infrastructure isolated from Domain
- Reusable EF Core setup
- Optional adoption

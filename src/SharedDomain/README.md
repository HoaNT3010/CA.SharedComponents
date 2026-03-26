# CA.SharedComponents.SharedDomain

Provides core **Domain-Driven Design (DDD)** building blocks and common primitives used across applications.

This project is dependency-free and represents the **true shared kernel** of the solution.

## Purpose

The goal of this project is to standardize how domain models are implemented by providing:

- **DDD** building block abstractions
- Base `Entity` and `AggregateRoot`
- `ValueObject` implementation
- Domain event abstractions
- `Result` and `Error` patterns
- Common utilities

## Dependencies

This project intentionally has **no external dependencies**.

It should be possible to reference this project from:

- Web APIs
- Console apps
- Microservices
- Any .NET project

## What’s Included

| Component              | Description                                |
| ---------------------- | ------------------------------------------ |
| Entity / AggregateRoot | Base classes for domain entities           |
| ValueObject            | Equality-based value object implementation |
| IDomainEvent           | Domain event abstraction                   |
| Result / Error         | Standardized success/failure handling      |

## Design Principles

- Dependency-free
- Framework-agnostic
- Focused on domain modeling

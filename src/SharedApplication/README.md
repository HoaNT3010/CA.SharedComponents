# CA.SharedComponents.SharedApplication

Provides **CQRS abstractions** and application-layer building blocks built on top of the Domain project.

This project standardizes how commands, queries, and behaviors are implemented.

## Purpose

To provide reusable abstractions for:

- Commands and Queries
- Application-level interfaces
- Domain event dispatching implementation

## Dependencies

- CA.SharedComponents.Domain
- [MediatR](https://www.nuget.org/packages/MediatR)
- [Ardalis.Specification](https://www.nuget.org/packages/Ardalis.Specification)

## What’s Included

| Component                       | Description                                              |
| ------------------------------- | -------------------------------------------------------- |
| ICommand / IQuery               | CQRS request abstractions                                |
| ICommandHandler / IQueryHandler | CQRS handler abstractions                                |
| Application interfaces          | Shared contracts (Repository, DomainEventDispatcher,...) |
| MediatRDomainEventDispatcher    | Domain event dispatcher implementation with MediatR      |

## Design Principles

- No infrastructure concerns
- Standardizes CQRS usage

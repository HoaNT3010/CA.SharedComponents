# CA.SharedComponents

A collection of reusable building blocks for .NET applications following **Clean Architecture**, **DDD**, **CQRS** principles, just to name a few.

> 📚 **Learning Project:**
> This repository is primarily developed as a personal learning project to deepen understanding of software architecture patterns and best practices in modern .NET development.

## Overview

`CA.SharedComponents` is designed as a **modular shared library solution**, consisting of multiple layers:

- **Domain** → Core DDD building blocks (no dependencies)
- **Application** → CQRS abstractions and behaviors
- **Infrastructure** → Persistence and technical implementations

The goal is to explore how to design **reusable, maintainable, and scalable components** that can be shared across multiple projects.

## Goals

- Practice and apply **Clean Architecture**
- Gain hands-on experience with **Domain-Driven Design (DDD)**
- Implement **CQRS patterns** using MediatR
- Understand trade-offs in designing shared libraries
- Build a reusable foundation for future projects

## Architecture

The solution follows a layered structure:

```
CA.SharedComponents
│
├── src
│   │
│   ├── SharedDomain
│   │
│   ├── SharedApplication
│   │
│   └── SharedInfrastructure
│
└── tests
    │
    ├── SharedDomain.Tests
    │
    ├── SharedApplication.Tests
    │
    └── SharedInfrastructure.Tests
```

Please checkout each project's README file for additional information.

## Packages

### Packages Overview

This repository is intended to be split into multiple NuGet packages:

| Package                                                                                                   | Description                              |
| --------------------------------------------------------------------------------------------------------- | ---------------------------------------- |
| [`CA.SharedComponents.Domain`](https://www.nuget.org/packages/CA.SharedComponents.Domain)                 | Core domain primitives (dependency-free) |
| [`CA.SharedComponents.Application`](https://www.nuget.org/packages/CA.SharedComponents.Application)       | CQRS + MediatR abstractions              |
| [`CA.SharedComponents.Infrastructure`](https://www.nuget.org/packages/CA.SharedComponents.Infrastructure) | EF Core + persistence implementations    |

### Installing Packages

CA.SharedComponents.Domain

```
dotnet add package CA.SharedComponents.Domain
```

CA.SharedComponents.Application

```
dotnet add package CA.SharedComponents.Application
```

CA.SharedComponents.Infrastructure

```
dotnet add package CA.SharedComponents.Infrastructure
```

> Consumers should only reference what they need.

## Dependencies

- [.NET 10](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/overview)
- [MediatR](https://www.nuget.org/packages/MediatR)
- [Ardalis.Specification](https://www.nuget.org/packages/Ardalis.Specification)
- [Ardalis.Specification.EntityFrameworkCore](https://www.nuget.org/packages/Ardalis.Specification.EntityFrameworkCore)

## Contributing

This is primarily a personal learning project, but suggestions and discussions are welcome.

## Acknowledgements

These are some of the sources that I used in order to develop this project:

- [Microsoft documentation](https://learn.microsoft.com/en-us/)
- [DevIQ](https://deviq.com)
- [Ardalis](https://www.youtube.com/@Ardalis/featured)
- [Milan Jovanović](https://www.youtube.com/@MilanJovanovicTech/featured)

## Final Note

This project is part of my journey to become a better software engineer by:

- Writing clean, maintainable code
- Applying architectural patterns in practice
- Continuously refining design decisions through real usage

Feedback and ideas are always appreciated!

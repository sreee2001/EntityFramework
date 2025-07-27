# Entity Framework 6 Showcase

> A Collection of Examples of EntityFramework in C# Dotnet

## 🌟 Author

I am [Srikanth Tangella](https://github.com/sreee2001) and I am an Software Engineer with many years of Experience in Desktop software
After so many years of programming, I created a set of reference projects for Entity Framework using C# Dotnet Framework.
The purpose is to log my research and provide easy to grab snippets and or base projects for future use

## 🌟 Contents

### [01_Library](./01_Library)
> Reusable libraries with useful generic implementations
- [Infrastructure](./01_Library/Infrastructure) : Contains generic foundational code that can be used in any project
- [Repository](./01_Library/Repository) : Entity Framework specific foundational classes that is useful in connecting to EF and making CRUD operations

### [02_Examples](./02_Examples)
> Sample projects that showcase Entity Framework 6
- [CodeFirstExample](./02_Examples/CodeFirstExample) : A minimalistic example of Code First approach for EF
- [CodeFirstWithDatabaseSeeding](./02_Examples/CodeFirstWithDatabaseSeeding) : Continuing of the CodeFirstExample, but this time seeding the database at the time of creation
- [TestingCodeFirst_FirstProject](./02_Examples/TestingCodeFirst_FirstProject) : An example from Microsoft on Code first EF

### [Demo](./Demo)
> My custom demo for Entity framework 6 using C# Dotnet Framework console
- [ExampleRepository](./Demo/ExampleRepository): A library that holds my Model and DataContext classes using Infrastructure and Repository Projects
- [DemoForRepository](./Demo/DemoForRepository): Program execution code

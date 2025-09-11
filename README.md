[![NuGet Version and Downloads count](https://img.shields.io/nuget/v/Platform.Data?label=nuget&style=flat)](https://www.nuget.org/packages/Platform.Data)
[![Actions Status](https://github.com/linksplatform/Data/workflows/CD/badge.svg)](https://github.com/linksplatform/Data/actions?workflow=CD)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/b38e839402d9451aa3e58fe05521325f)](https://app.codacy.com/gh/linksplatform/Data?utm_source=github.com&utm_medium=referral&utm_content=linksplatform/Data&utm_campaign=Badge_Grade_Settings)
[![CodeFactor](https://www.codefactor.io/repository/github/linksplatform/data/badge)](https://www.codefactor.io/repository/github/linksplatform/data)

# [Data](https://github.com/linksplatform/Data)

LinksPlatform's Platform.Data Class Library.

## What is Platform.Data?

Platform.Data is a foundational library that provides abstract interfaces and core functionality for working with **Links** - a universal data structure concept for storing and manipulating interconnected information. This library serves as the fundamental building block of the LinksPlatform ecosystem, enabling developers to create efficient, graph-like data storage and retrieval systems.

## Key Capabilities

### 🔗 Universal Data Abstraction
- **Generic Link Interface**: The `ILinks<TLinkAddress, TConstants>` interface provides a unified way to work with any type of link-based data structures
- **Flexible Addressing**: Support for different address types through generic programming, allowing optimization for various use cases
- **Size-Independent**: Works seamlessly with doublets (2-element links), triplets (3-element links), and sequences of any size

### 🚀 Core Operations
- **CRUD Operations**: Create, Read, Update, and Delete operations on link structures
- **Pattern Matching**: Advanced querying capabilities with flexible restriction patterns
- **Batch Processing**: Efficient handling of multiple links through callback-based iteration
- **Transaction Support**: Coordinated operations with handler-based change tracking

### 📊 Data Management Features
- **Count Operations**: Efficiently count links matching specific criteria
- **Traversal Support**: Navigate through interconnected data structures
- **Constants Management**: Centralized configuration and constraints handling
- **Exception Handling**: Specialized exceptions for data integrity and constraint violations

### 🏗️ Architecture Benefits
- **Interface Segregation**: Clean separation of concerns through focused interfaces
- **Generic Design**: Type-safe operations with compile-time guarantees
- **Extensible Foundation**: Base classes and extension methods for easy customization
- **Performance Optimized**: Aggressive inlining and efficient memory usage patterns

Namespace: [Platform.Data](https://linksplatform.github.io/Data/csharp/api/Platform.Data.html)

Forked from: [Konard/LinksPlatform/Platform/Platform.Data](https://github.com/Konard/LinksPlatform/tree/4d902dd3f4267284a494c35e1ae1887d5a309bef/Platform/Platform.Data)

NuGet package: [Platform.Data](https://www.nuget.org/packages/Platform.Data)

## Getting Started

### Prerequisites
- .NET 8.0 or later
- Understanding of generic programming concepts
- Familiarity with callback/handler patterns

### Installation
```bash
# Via NuGet Package Manager
dotnet add package Platform.Data

# Via Package Manager Console
Install-Package Platform.Data

# Via PackageReference in .csproj
<PackageReference Include="Platform.Data" Version="*" />
```

### Understanding Link Structures
In Platform.Data, a **link** represents a relationship between entities:
- **Doublet**: `[Source, Target]` - Simple relationship
- **Triplet**: `[Id, Source, Target]` - Relationship with identity
- **N-tuple**: `[Id, Part1, Part2, ..., PartN]` - Complex relationships

### Essential Concepts
- **TLinkAddress**: Generic type for link addressing (typically `uint`, `ulong`)
- **Restrictions**: Query patterns using constants like `Any`, `Null`, or specific addresses  
- **Handlers**: Callback functions for processing results and changes
- **Constants**: Configuration object containing special values and constraints

### Basic Usage

```csharp
using Platform.Data;

// Example: Working with a generic links interface
public class MyLinksProcessor<TLinkAddress, TConstants> 
    where TLinkAddress : IUnsignedNumber<TLinkAddress>
    where TConstants : LinksConstants<TLinkAddress>
{
    private readonly ILinks<TLinkAddress, TConstants> _links;

    public MyLinksProcessor(ILinks<TLinkAddress, TConstants> links)
    {
        _links = links;
    }

    // Count all links in storage
    public TLinkAddress CountAllLinks()
    {
        return _links.Count(null); // No restrictions = count all
    }

    // Create a new link with specific source and target
    public TLinkAddress CreateLink(TLinkAddress source, TLinkAddress target)
    {
        var substitution = new TLinkAddress[] { default, source, target };
        return _links.Create(substitution, null);
    }

    // Query links with pattern matching
    public void ProcessLinksWithSource(TLinkAddress sourceId)
    {
        var restriction = new TLinkAddress[] { 
            _links.Constants.Any,    // Any link ID
            sourceId,                // Specific source
            _links.Constants.Any     // Any target
        };
        
        _links.Each(restriction, link => {
            // Process each matching link
            Console.WriteLine($"Found link: {link[0]} -> {link[1]} -> {link[2]}");
            return _links.Constants.Continue;
        });
    }
}
```

## Use Cases

### 🗄️ Graph Database Foundation
Build custom graph databases with optimized link-based storage, perfect for knowledge graphs, social networks, and semantic data structures.

### 🔄 Data Integration
Create universal adapters for different data storage systems using the common `ILinks` interface, enabling seamless data migration and synchronization.

### 🧠 Knowledge Representation
Implement sophisticated knowledge representation systems where concepts, relationships, and hierarchies are stored as interconnected links.

### ⚡ High-Performance Computing
Leverage the generic, inlined operations for building high-performance data processing pipelines that work with structured relationships.

## [Documentation](https://linksplatform.github.io/Data)
Interface [ILinks\<TLinkAddress, TConstants\>](https://linksplatform.github.io/Data/csharp/api/Platform.Data.ILinks-2.html).

[PDF file](https://linksplatform.github.io/Data/csharp/Platform.Data.pdf) with code for e-readers.

## Architecture Overview

Platform.Data follows a **layered architecture** designed for maximum flexibility and performance:

### Core Interface Layer
- **`ILinks<TLinkAddress, TConstants>`**: The foundational interface defining all link operations
- **Generic Type System**: Supports different address types (`uint`, `ulong`, etc.) through `IUnsignedNumber<T>`
- **Constants Management**: Centralized configuration through `LinksConstants<T>` classes

### Operation Categories
1. **Read Operations**: `Count()`, `Each()` - Query and traverse link structures
2. **Write Operations**: `Create()`, `Update()`, `Delete()` - Modify link relationships
3. **Handler-Based Processing**: Callback mechanisms for batch operations and change tracking

### Key Design Principles
- **Separation of Concerns**: Clear distinction between data access interface and implementation
- **Type Safety**: Compile-time guarantees through generic constraints
- **Performance First**: Aggressive inlining and minimal allocations
- **Extensibility**: Extension methods and base classes for customization

### Integration Points
The library serves as a **foundational abstraction** that enables:
- Multiple storage backend implementations (memory, file-based, distributed)
- Different link relationship models (doublets, triplets, n-tuples)
- Custom addressing schemes and optimization strategies
- Interoperability between different LinksPlatform components

## Depend on
*   [Platform.Threading](https://github.com/linksplatform/Threading)
*   [Platform.Numbers](https://github.com/linksplatform/Numbers)
*   [Platform.Setters](https://github.com/linksplatform/Setters)

## Dependent libraries
*   [Platform.Data.Doublets](https://github.com/linksplatform/Data.Doublets)
*   [Platform.Data.Triplets](https://github.com/linksplatform/Data.Triplets)

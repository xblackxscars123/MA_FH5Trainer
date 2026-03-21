# Technology Stack

## Programming Languages
- **C# 12** (implicit usings enabled)
- **XAML** for UI markup
- **PowerShell** for build scripts

## Target Framework
- **.NET 8.0 Windows** (`net8.0-windows`)
- x64 platform only
- WPF enabled

## Core Dependencies

### UI Framework
- **WPF** (Windows Presentation Foundation) - Core UI framework
- **MahApps.Metro 2.4.10** - Modern UI styling and controls
- **System.Xaml** - XAML runtime support

### MVVM Infrastructure
- **CommunityToolkit.Mvvm 8.2.2** - MVVM helpers and source generators

### Application Framework
- **Microsoft.Extensions.Hosting 8.0.0** - Dependency injection and hosting
- **Microsoft.Extensions.Configuration** - Configuration management
- **Microsoft.Extensions.DependencyInjection** - DI container

### System Integration
- **System.Management 8.0.0** - Windows management instrumentation

### Memory Operations
- **Reloaded.Memory.Sigscan 3.1.9** - Pattern scanning in memory

## Build Configuration

### Project Settings
- **Platform**: x64 only
- **Output Type**: WinExe (Windows executable)
- **Assembly Name**: xpaint
- **Root Namespace**: XPaint
- **Nullable**: Enabled
- **Implicit Usings**: Enabled

### Compiler Options
- **AllowUnsafeBlocks**: true (for memory operations)
- **CheckForOverflowUnderflow**: true (runtime overflow checking)
- **TreatWarningsAsErrors**: true (strict compilation)
- **Optimize**: true (for x64 platform)

### Security & Signing
- **SignAssembly**: true
- **PublicSign**: true
- **AssemblyOriginatorKeyFile**: MA_FH5Trainer.snk

### Release Configuration
- **DebugType**: none
- **GenerateDocumentationFile**: false
- **Optimize**: true
- **EnableCompressionInSingleFile**: true

### Runtime Features
- **UseHardenedRuntime**: true
- **EventSourceSupport**: false
- **HttpActivityPropagationSupport**: false
- **EnableUnsafeBinaryFormatterSerialization**: false
- **EnableUnsafeUTF7Encoding**: false
- **MetadataUpdaterSupport**: false

### Trimming Configuration
- Trimmer root assemblies: PresentationFramework, PresentationCore, WindowsBase, System.Xaml, Reloaded.Memory.Sigscan

### Localization
- **NeutralLanguage**: en-US
- **SatelliteResourceLanguages**: en

## Development Commands

### Build
```bash
dotnet build RemixedTrnr/MA_FH5Trainer.sln -c Release -p:Platform=x64
```

### Clean
```bash
dotnet clean RemixedTrnr/MA_FH5Trainer.sln
```

### Restore Dependencies
```bash
dotnet restore RemixedTrnr/MA_FH5Trainer.sln
```

### Run
```bash
dotnet run --project RemixedTrnr/remixtrnr/MA_FH5Trainer.csproj
```

## Build Scripts
- **cleanup.ps1** - Clean build artifacts
- **fixroots.ps1** - Fix trimmer root assemblies

## Project Dependencies
- Main project (`xpaint`) depends on `Memory` library
- Dependency configured in solution file with ProjectDependencies section

## Version Information
- **AssemblyVersion**: 1.0.0.0
- **FileVersion**: 1.0.0.0
- **Product**: xpaint 1.0

## Requirements
- **OS**: Windows 10 or later (x64)
- **Runtime**: .NET 8.0 Runtime
- **IDE**: Visual Studio 2022 (version 17.11+) or compatible

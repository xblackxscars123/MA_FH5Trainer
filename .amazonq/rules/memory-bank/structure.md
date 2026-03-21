# Project Structure

## Solution Architecture
The project is organized as a Visual Studio solution with two main projects:
- **xpaint** (main WPF application)
- **Memory** (memory manipulation library)

## Directory Organization

### `/RemixedTrnr/remixtrnr/` - Main Application
Core WPF application implementing the xpaint functionality.

#### `/Cheats/`
Modular cheat system with specialized functionality:
- `ICheatsBase.cs`, `IRevertBase.cs` - Base interfaces for cheat modules
- `CarCheats.cs`, `CameraCheats.cs` - Vehicle and camera manipulation
- `CustomizationCheats.cs`, `TuningCheats.cs` - Customization features
- `EnvironmentCheats.cs`, `PhotomodeCheats.cs` - Environment controls
- `UnlocksCheats.cs`, `MiscCheats.cs` - Unlocks and miscellaneous features
- `Bypass.cs`, `Sql.cs` - Security bypass and database operations
- `CheatsUtilities.cs` - Shared utilities

#### `/Controls/`
Custom WPF controls:
- `StatusComboboxItem/` - Status display combo box
- `TranslationComboboxItem/` - Localized combo box items

#### `/Converters/`
XAML value converters for data binding:
- `AndBooleanMultiConverter.cs` - Boolean logic converter
- `BoolParamConverter.cs`, `IntParamConverter.cs` - Parameter converters
- `InstanceEqualsConverter.cs`, `TypeToInstanceConverter.cs` - Type converters
- `MultiplyConverter.cs`, `TupleConv.cs` - Mathematical and tuple converters

#### `/Models/`
Data models:
- `GameVerPlat.cs` - Game version and platform information

#### `/Resources/`
Application resources and utilities:
- `Keybinds/` - Global hotkey management (`GlobalHotkey.cs`, `HotkeysManager.cs`)
- `Theme/` - UI theming (`ThemeConstants.xaml`, `Theming.cs`, `CommonBorderStyle.xaml`)
- `Translations/` - Localization resources (`English.xaml`)
- `Cheats.cs`, `Memory.cs`, `Pages.cs` - Core resource classes
- `Imports.cs` - Native API imports
- `StringCipher.cs` - Encryption utilities

#### `/Services/`
Application services:
- `ApplicationHostService.cs` - Application lifecycle management
- `WindowsProviderService.cs` - Windows integration services

#### `/ViewModels/`
MVVM view models organized by feature:
- `Windows/MainWindowViewModel.cs` - Main window logic
- `Pages/` - Page-level view models (Autoshow, Garage, Tuning)
- `SubPages/SelfVehicle/` - Vehicle-specific features (Camera, Customization, Environment, Handling, Misc, PhotoMode, Unlocks, Wheelspins)
- `SubPages/MultipliersViewModel.cs` - Multipliers functionality

#### `/Views/`
XAML views matching view model structure:
- `Windows/MainWindow.xaml` - Main application window
- `SubPages/SelfVehicle/` - Vehicle feature views
- `SubPages/Tuning/` - Tuning system views (Aero, Alignment, Damping, Gearing, Springs, Steering, Tires, Others)
- `ExpandersView.xaml` - Expandable UI component

### `/RemixedTrnr/Memory/` - Memory Library
Low-level memory manipulation library:
- `/Methods/` - Memory operation methods
- `/Types/` - Memory-related type definitions
- `/Resources/` - Library resources
- `Memory.cs` - Core memory class
- `Utils.cs` - Utility functions
- `NativeMethods.txt` - Native method documentation

## Architectural Patterns

### MVVM (Model-View-ViewModel)
- Strict separation between UI (Views) and logic (ViewModels)
- Data binding through XAML
- CommunityToolkit.Mvvm for MVVM infrastructure

### Dependency Injection
- Microsoft.Extensions.Hosting for DI container
- Service registration in `App.xaml.cs`
- Hosted services for application lifecycle

### Modular Cheat System
- Interface-based design (`ICheatsBase`, `IRevertBase`)
- Cached instances for performance
- Cleanup and revert capabilities

### Resource Management
- Centralized resource classes (Cheats, Memory, Pages)
- Theme and translation resource dictionaries
- Global hotkey management system

## Key Components

### Application Entry Point
- `App.xaml.cs` - Application initialization, DI setup, exception handling
- Single instance enforcement via Mutex
- Graceful shutdown with cleanup

### Memory Subsystem
- Separate library project for memory operations
- Pattern scanning with Reloaded.Memory.Sigscan
- Process handle management

### UI Framework
- WPF with MahApps.Metro for modern styling
- Custom controls for specialized functionality
- Responsive layout with expanders and navigation

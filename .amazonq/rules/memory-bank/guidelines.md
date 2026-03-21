# Development Guidelines

## Code Quality Standards

### File Encoding
- Use UTF-8 with BOM for C# source files
- XAML files use UTF-8 encoding
- Maintain consistent line endings (CRLF for Windows)

### Formatting Conventions
- Use 4 spaces for indentation (no tabs)
- Opening braces on same line for methods and classes
- Single-line property accessors: `{ get; set; }`
- Expression-bodied members for simple properties: `public Type Property => value;`
- Compact array initialization: `new byte[] { ... }` on single or multiple lines

### Naming Standards
- PascalCase for public members, classes, methods, properties
- camelCase with underscore prefix for private fields: `_fieldName`
- PascalCase for constants: `VelEnabled`, `HookSize`
- Descriptive names for async methods ending with async operation context
- Static readonly fields with `s_` prefix: `s_hotkeys`, `s_hookLock`

### Documentation
- XML documentation comments for public APIs (optional but recommended)
- Inline comments for complex logic, especially in low-level operations
- Error messages should be descriptive and include context
- Use `/// <summary>` tags for method documentation when needed

## Architectural Patterns

### MVVM Implementation
**Strict separation of concerns:**
```csharp
// View code-behind - minimal logic
public partial class Autoshow : Page
{
    public Autoshow()
    {
        ViewModel = new AutoshowViewModel();
        DataContext = this;
        InitializeComponent();
    }
    
    public AutoshowViewModel ViewModel { get; }
}
```

**ViewModel pattern with CommunityToolkit.Mvvm:**
```csharp
public partial class AutoshowViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _uiElementsEnabled = true;
    
    [RelayCommand]
    private async Task ExecuteSql(object parameter)
    {
        // Command implementation
    }
}
```

### Dependency Injection
- Use Microsoft.Extensions.Hosting for DI container
- Register services in `App.xaml.cs` startup
- Access services via `App.GetRequiredService<T>()`
- Singleton pattern for window services

**Service registration:**
```csharp
services.AddHostedService<ApplicationHostService>();
services.AddSingleton<MetroWindow, MainWindow>();
```

**Service retrieval:**
```csharp
if (App.GetRequiredService<T>() is not Window windowInstance)
{
    throw new InvalidOperationException("Window is not registered as service.");
}
```

### Singleton Pattern with Caching
Use static resource classes with cached instances:
```csharp
private static Cheats.Core.Sql SqlDefault => GetClass<Cheats.Core.Sql>();
```

## Memory Management Patterns

### Unsafe Code and Pointers
- Use `UIntPtr` for memory addresses (not `IntPtr` for unsigned addresses)
- Use `nuint` for native unsigned integers
- Always validate addresses before operations: `if (address == 0) return false;`
- Clean up allocated memory in `Cleanup()` methods

**Memory address handling:**
```csharp
private UIntPtr _localPlayerHookAddress;
public UIntPtr LocalPlayerHookDetourAddress;

_localPlayerHookAddress = await SmartAobScan(sig);
if (_localPlayerHookAddress == 0)
{
    ShowError("CheatLocalPlayer", "_localPlayerHookAddress == 0");
    return false;
}
```

### Resource Cleanup Pattern
Implement `ICheatsBase` and `IRevertBase` interfaces:
```csharp
public class CarCheats : ICheatsBase, IRevertBase
{
    public void Cleanup()
    {
        // Restore original bytes and free allocated memory
        if (AccelDetourAddress > 0)
        {
            mem.WriteArrayMemory(_accelAddress, new byte[] { 0xF3, 0x0F, 0x10, 0x5D, 0x0C });
            Free(AccelDetourAddress);
        }
    }
    
    public void Revert()
    {
        // Restore without freeing (temporary revert)
    }
    
    public void Continue()
    {
        // Re-apply detours after revert
    }
}
```

## Async/Await Patterns

### Async Method Conventions
- Use `async Task` for async operations (not `async void` except event handlers)
- Await all async operations properly
- Disable UI during async operations:
```csharp
[RelayCommand]
private async Task ExecuteSql(object parameter)
{
    UiElementsEnabled = false;
    await Query(sParam);
    UiElementsEnabled = true;
}
```

### Task Coordination
- Use `Task.Run()` for CPU-bound work
- Use `Dispatcher.InvokeAsync()` for UI thread operations
- Handle `TaskCanceledException` gracefully

## WPF-Specific Patterns

### Custom Controls
**Dependency Property pattern:**
```csharp
public class TranslationComboboxItem : ComboBoxItem
{
    public static readonly DependencyProperty TranslatorsProperty
        = DependencyProperty.Register(nameof(Translators),
            typeof(string),
            typeof(TranslationComboboxItem),
            new PropertyMetadata(default(string)));
    
    [Bindable(true)]
    [Category("XPaint")]
    public string Translators
    {
        get => (string)GetValue(TranslatorsProperty);
        set => SetValue(TranslatorsProperty, value);
    }

    static TranslationComboboxItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(TranslationComboboxItem), 
            new FrameworkPropertyMetadata(typeof(TranslationComboboxItem)));
    }
}
```

### Value Converters
Implement `IValueConverter` or `IMultiValueConverter`:
```csharp
public class AndBooleanMultiConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.All(v => v is bool) && values.Length == 2)
        {
            return (bool)values[0] && (bool)values[1];
        }
        return false;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
```

## Error Handling

### Validation Pattern
- Check preconditions early and return/throw appropriately
- Use guard clauses for null checks
- Validate state before operations:
```csharp
if (MainWindow.Instance?.ViewModel.Attached != true)
    return;
```

### Exception Handling
- Catch specific exceptions when possible
- Use global exception handlers in `App.xaml.cs`
- Display user-friendly error messages
- Log errors with context information

**Global exception handling:**
```csharp
AppDomain.CurrentDomain.UnhandledException += (_, e) =>
    ReportException((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");

DispatcherUnhandledException += (_, e) =>
{
    ReportException(e.Exception, "Application.Current.DispatcherUnhandledException");
    e.Handled = true;
};
```

## Native Interop Patterns

### P/Invoke with LibraryImport
Use modern `[LibraryImport]` attribute (C# 11+):
```csharp
[LibraryImport("user32.dll", EntryPoint = "SetWindowsHookExW", SetLastError = true)]
private static partial IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

[LibraryImport("user32.dll", SetLastError = true)]
[return: MarshalAs(UnmanagedType.Bool)]
private static partial bool UnhookWindowsHookEx(IntPtr hhk);
```

### Thread Safety
- Use `lock` statements for shared resources
- Use dedicated lock objects: `private static readonly object s_hookLock = new object();`
- Protect state changes in multi-threaded scenarios:
```csharp
lock (s_hookLock)
{
    if (s_hookId != IntPtr.Zero)
    {
        return true;
    }
    // Critical section
}
```

## Assembly Configuration

### AssemblyInfo Patterns
```csharp
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]

#if !RELEASE
[assembly: XmlnsDefinition("debug-mode", "Namespace")]
#endif
```

## Data Structures

### Struct Usage
Use `struct` for lightweight data containers with `init` properties:
```csharp
internal struct MemoryRegionResult
{
    public nuint CurrentBaseAddress { get; init; }
    public long RegionSize { get; init; }
    public nuint RegionBase { get; init; }
}
```

## Static Utility Classes
- Mark utility classes as `static` when they contain only static members
- Use generic constraints for type safety:
```csharp
public static void Show<T>() where T : class
{
    if (!typeof(Window).IsAssignableFrom(typeof(T)))
    {
        throw new InvalidOperationException($"The window class should be derived from {typeof(Window)}.");
    }
}
```

## Code Organization

### Namespace Structure
- Match namespace to folder structure: `XPaint.Views.SubPages.SelfVehicle`
- Use file-scoped namespaces when appropriate
- Group related functionality in dedicated folders

### Using Directives
- Place `using` statements at top of file
- Use `global using` in project file for common namespaces
- Static imports for frequently used static classes: `using static XPaint.Resources.Cheats;`

## Best Practices

### Null Safety
- Enable nullable reference types: `<Nullable>enable</Nullable>`
- Use null-conditional operators: `MainWindow.Instance?.ViewModel`
- Use pattern matching: `if (parameter is not string sParam)`
- Use `ArgumentNullException.ThrowIfNull()` for parameter validation

### Collection Initialization
- Use collection expressions: `private static readonly List<GlobalHotkey> s_hotkeys = [];`
- Use LINQ for queries: `values.All(v => v is bool)`

### Retry Logic
Implement retry patterns for unreliable operations:
```csharp
private static int s_hookRetryCount = 0;
private const int MAX_HOOK_RETRIES = 3;

private static bool AttemptHookSetup()
{
    s_hookRetryCount++;
    if (s_hookRetryCount >= MAX_HOOK_RETRIES)
    {
        // Show error and return
        return false;
    }
    // Attempt operation with delay on failure
}
```

### Single Instance Enforcement
Use Mutex for single instance applications:
```csharp
private const string MutexName = "{(B7C3D9E2-1F4A-48B6-A5E0-7D2C8F6E3B19)}";
private Mutex _mutex = null!;

protected override void OnStartup(StartupEventArgs e)
{
    _mutex = new Mutex(true, MutexName, out var createdNew);
    if (!createdNew)
    {
        MessageBox.Show("Another instance is already running.");
        Current.Shutdown();
    }
}
```

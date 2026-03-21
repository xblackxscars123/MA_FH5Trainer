# Code Review Findings for RemixedTrnr

## CRITICAL
1. **Compilation/Runtime bug in `ApplicationHostService`**
   - In `HandleActivationAsync`, `serviceProvider.GetService(...)` is called but constructor parameter is not stored into a field and `serviceProvider` is undefined.
2. **Unsafe process-handling detach/cleanup**
   - In `App.DisconnectFromGame`, iterating `g_CachedInstances` and casting to `ICheatsBase` and then closing handle can throw and can be brittle.
3. **Exception report can throw**
   - `ReportException` accesses `exception.StackTrace` and `GameVerPlat.GetInstance()` without null-safe checks.

## WARNING
1. **Async void method usage**
   - `MainWindowViewModel.InitializeViewModel` is `async void`, which can hide exceptions.
2. **Timer callback thread-safety/UI updates**
   - `SetupAttach` does process scanning and UI state updates in `Timer.Elapsed`, risking cross-thread updates and reentrancy.
3. **Global singleton/mutable static state**
   - `GetInstance()` singletons and static shared state in memory/cheats can make lifecycle bugs and tests hard.
4. **Constructor side effects**
   - `MainWindowViewModel()` immediately calls initialization and timers, making construction heavier and harder to unit test.
5. **No HTTP timeout/empty update URLs**
   - `CheckGit` uses `HttpClient` with no timeout and `GitHubRepoUrl` constants empty; this should be defensive.

## INFO (best practices)
1. Good use of CommunityToolkit MVVM attributes to reduce boilerplate.
2. Strong exception trapping with global handlers in `App`.
3. Theming initialization is encapsulated using `ThemeManager`.

## GOOD
1. `CheatsUtilities` methods are concise and domain-specific.
2. `GvpMaker` handles game metadata gracefully with fallback values.
3. Generic host + DI pattern in `App` plus a host service is solid architecture.

## Suggested Immediate Fixes
- Fix `ApplicationHostService` by storing constructor-injected `IServiceProvider` in a field and use it.
- Change `async void InitializeViewModel` to `async Task` and call it from non-async constructor flow.
- Avoid UI updates from `Timer.Elapsed`; dispatch to UI thread or use `DispatcherTimer`.
- Add null safety in exception display logic and cleanup paths.
- Add timeout for update check and make update URL constants configurable.

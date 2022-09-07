# My Forest
Idle game to create a forest little by little.

### Table of Contents
1. [Structure Conventions](#structure-conventions)
2. [Code Conventions](#code-conventions)

## Structure Conventions

The project uses a code structure hierarchy using **UniRx** and **Zenject**.

#### **Managers**
They control the main systems, live in the main **Zenject** container in `ProjectContext` prefab and are set in `MainInstaller` script and pass data or methods to others using interfaces, we have several types of interfaces:

- **Sources**
    They pass utility methods.
- **DataSources**
    They pass data or observables that pass data.
- **EventSources**
    They pass observables that dont pass data.
- **DebugSources**
    They pass anything debug code uses.

All interfaces are applied to managers using explicit implementations.

There are files to add these sources, named after each system `[SYSTEM_NAME]Sources.cs`, by always making bridges using interfaces we can distribute easily with **Zenject** while maintaining control over the structure of the scripts.

There are files to add data structures, named after each system `[SYSTEM_NAME]DataStructures.cs`, you can find enums, classes and structs here.

To use observables to their full we use **UniRx** `Subject` and our `DataSubject` to store the observable references.

#### **Controllers**
Controllers are not bound to systems, controllers can connect to different managers if they need to, but no manager can connect to controller, we need to maintain the dependency only in one direction.

They can subscribe to data or events coming from the managers and act accordingly using `[Inject]` along with the source interface needed.

For everything to be handled correctly **Zenject** requires controllers to subscribe on `Start` not on `Awake`.

Is ok for the controllers to have their own scrips, hierarchy below or connections via inspector, only if all their dependencies passes through the controller of that system.

#### **Extensions**
When possible try to abstract useful utility things into an extension, these must be part of the namespace the original class derives.

For example `GameObjectExtensions` would be inside `UnityEngine` namespace.

#### **UI**
Every component that directly connects to UI must end with a suffix that connects with UI.

- `ComponentUI`
- `ComponentButton`
- `ComponentContainer`
- `ComponentScroll`
- `ComponentToggle`

This way we can get to them easier by name, also take that level of abstraction when creating a component for UI.

#### **Debug**
We have all debug code inside an special namespace so we can track it very easily and also prevent it appearing in autocompletion if some classes names are similar.

Most of the code here will be used by special UI components that will appear when debug menu is activated and are connected to debug source interfaces, these are intended to use on **Editor** and **Device**.

## Code Conventions

These are only special cases that tend to change pretty easily bewteen code styles.

- Everything must be under a namespace with project name `namespace MyProject`
- Everything debug related must be under a debug namespace with project name `namespace MyProject.Debug`
- All implementation of interfaces must be used on partial classes `public partial class Manager : ISource {}`
- All interface implementations on partial classes must be explicit `ISource.MyImplementation`
- Interfaces are PascalCase and start with a letter I `public class IExampleInterface`
- Classes are PascalCase `public class ExampleClass`
- Properties are PascalCase `public int ExampleProperty { get; private set; }`
- Methods are PascalCase `public void ExampleMethod()`
- Fields are camelCase `public int _field;`
- Properties in interfaces must be one line when possible `ISource.ExampleProperty => ExampleProperty`
- Methods in interfaces must be one line when possible `ISource.ExampleMethod() => ExampleMethod()`
- All member fields must have underscore first `_field`
- All member fields must be initialized `_field = null` or `_field = default`
- Serialized fields must be private and on same line `[SerializedField] private`
- Injected source fields must be private and on same line `[Inject] private`
- Constants are all uppercase and sepaarted by underscores `const MY_CONSTANT = "example"`
- Libraries are separated with extra line between plugins and system libraries.
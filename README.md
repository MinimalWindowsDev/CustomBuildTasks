# Custom MSBuild Tasks Tutorial

This repository demonstrates how to create and use custom MSBuild tasks in .NET Framework projects using Visual Studio 2019. The example implements a simple line counting task that runs during the build process.

## 🎯 What You'll Learn

- Creating custom MSBuild tasks
- Integrating tasks using .targets files
- Proper project structure for build task libraries
- Task execution ordering and build pipeline integration
- Debug techniques for MSBuild tasks

## 🏗️ Project Structure

```
CustomBuildTasks/
├── CustomBuildTasks.sln
├── src/
│   ├── CustomBuildTasks/           # Custom task library
│   │   ├── CustomBuildTasks.csproj
│   │   ├── LineCounter.cs
│   │   └── Properties/
└── example/
    └── ExampleProject/             # Example implementation
        ├── ExampleProject.csproj
        ├── Program.cs
        └── Custom.targets
```

## 🚀 Getting Started

### Prerequisites

- Visual Studio 2019 or later
- .NET Framework 4.7.2 or later
- Basic understanding of MSBuild concepts

### Installation

1. Clone this repository:

   ```batch
   git clone https://github.com/MinimalWindowsDev/CustomBuildTasks.git
   ```

2. Open `CustomBuildTasks.sln` in Visual Studio

3. Build the solution (F6 or Ctrl+Shift+B)

## 📘 How It Works

### 1. Custom Task Implementation (LineCounter.cs)

The `LineCounter` task demonstrates basic MSBuild task creation:

```csharp
public class LineCounter : Task
{
    [Required]
    public ITaskItem[] SourceFiles { get; set; }

    [Output]
    public int TotalLines { get; set; }

    public override bool Execute()
    {
        // Task implementation
    }
}
```

Key points:

- Inherits from `Microsoft.Build.Utilities.Task`
- Uses `[Required]` for mandatory inputs
- Uses `[Output]` for values accessible to MSBuild
- Returns boolean indicating success/failure

### 2. Task Integration (Custom.targets)

The `.targets` file connects your task to the build process:

```xml
<UsingTask TaskName="CustomBuildTasks.LineCounter"
           AssemblyFile="$(MSBuildProjectDirectory)\..\..\src\CustomBuildTasks\bin\Debug\CustomBuildTasks.dll" />

<Target Name="CountLines" BeforeTargets="Build">
    <LineCounter SourceFiles="@(Compile)">
        <Output TaskParameter="TotalLines" PropertyName="CodeLineCount" />
    </LineCounter>
</Target>
```

Key points:

- `UsingTask` registers your custom task
- `Target` defines when the task runs
- `BeforeTargets`/`AfterTargets` control execution order
- `Output` captures task results

## 🔧 Common Build Target Events

MSBuild follows this general order:

1. `BeforeBuild`
2. `CoreBuild`
3. `AfterBuild`

You can hook into these using:

```xml
<Target Name="CustomTarget" BeforeTargets="Build">
<Target Name="CustomTarget" AfterTargets="Build">
<Target Name="CustomTarget" DependsOnTargets="OtherTarget">
```

## 🐛 Debugging Tasks

1. Configure debugging in CustomBuildTasks project:

   - Set Start Action to:
     - Start external program: `C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe`
   - Set Command line arguments to:
     - `"$(SolutionDir)example\ExampleProject\ExampleProject.csproj" /t:Build`

2. Set breakpoints in your task code

3. Press F5 to start debugging

## 🔍 Advanced Topics

### Custom Task Properties

```csharp
// Required input
[Required]
public string InputPath { get; set; }

// Optional input
public bool VerboseOutput { get; set; }

// Output property
[Output]
public int ResultCount { get; set; }
```

### Conditional Target Execution

```xml
<Target Name="CustomTarget" Condition="'$(Configuration)' == 'Debug'">
```

### Multiple Target Dependencies

```xml
<Target Name="CustomTarget"
        DependsOnTargets="Target1;Target2"
        BeforeTargets="Build">
```

## 📝 Best Practices

1. **Task Naming**

   - Use clear, descriptive names
   - Follow the convention: `VerbNoun` (e.g., `CountLines`, `CompressImages`)

2. **Error Handling**

   - Always implement proper error handling in Execute()
   - Use `Log.LogError()` for meaningful error messages
   - Return `false` on failure

3. **Performance**

   - Keep build tasks lightweight
   - Implement incremental build support where possible
   - Use `TaskParameter` caching when appropriate

4. **Debugging**
   - Enable detailed MSBuild logging when needed:
     ```bash
     msbuild.exe solution.sln /v:diag > build.log
     ```

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License.

## 🙏 Acknowledgments

- Microsoft MSBuild documentation
- .NET Framework community
- Contributors and testers

## 📚 Further Reading

- [MSBuild SDK Documentation](https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild-sdk)
- [Custom Tasks Guide](https://docs.microsoft.com/en-us/visualstudio/msbuild/custom-tasks)
- [MSBuild Task Reference](https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild-task-reference)

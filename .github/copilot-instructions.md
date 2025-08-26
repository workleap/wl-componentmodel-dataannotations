# Workleap.ComponentModel.DataAnnotations

Workleap.ComponentModel.DataAnnotations is a .NET library that provides additional data annotation attributes for model validation and information attributes for properties, fields, and method parameters.

Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.

## Working Effectively

- **CRITICAL**: .NET 9.0.304 SDK is required (specified in global.json). Install with:
  - `curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --version 9.0.304`
  - `curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --runtime dotnet --version 8.0.12`
  - `export PATH="$HOME/.dotnet:$PATH"`

- Bootstrap, build, and test the repository:
  - Navigate to: `cd src/` (solution file is in src directory, not root)
  - `dotnet clean -c Debug` -- Never cancel, set timeout to 5 minutes.
  - `dotnet build -c Debug` --  Never cancel, set timeout to 5 minutes.
  - `dotnet test -c Debug --no-build --no-restore -l "console;verbosity=detailed"` -- takes ~2.6 seconds. NEVER CANCEL. Set timeout to 5+ minutes.

- **Do not attempt Release builds locally** – they will fail in development environments without proper GitVersion setup. Use Debug configuration for all local development work. Release builds are handled by CI/CD pipelines where the environment is correctly configured.

- Run formatting checks and fixes:
  - `dotnet format --verify-no-changes` -- check for formatting issues
  - `dotnet format` -- fix formatting issues automatically

## Validation

- Always run the complete build and test sequence after making changes:
  1. `cd src/`
  2. `dotnet clean -c Debug`
  3. `dotnet build -c Debug`
  4. `dotnet test -c Debug --no-build --no-restore -l "console;verbosity=detailed"`
  5. `dotnet format --verify-no-changes`

- **VALIDATION SCENARIOS**: Always test data annotation functionality after making changes:
  - Run the full test suite (220 tests) which covers all validation attributes
  - Test at least one example of each attribute type if modifying core validation logic
  - Verify PublicAPI.Shipped.txt is updated if adding new public APIs
  - Example validation scenario test:
    ```csharp
    var person = new Person { Id = "invalid-guid", Name = "" };
    var context = new ValidationContext(person);
    var results = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(person, context, results, validateAllProperties: true);
    // Should validate [Guid] and [NotEmpty] attributes
    ```

- **CRITICAL TIMING**: Set appropriate timeouts for all commands:
  - Build commands: 15+ minute timeout
  - Test commands: 5+ minute timeout
  - Avoid cancelling any build or test command prematurely; only cancel if there are clear issues (e.g., infinite loop, system hang, or error)

## Common Tasks

The following are outputs from frequently run commands. Reference them instead of viewing, searching, or running bash commands to save time.

### Repository Root Structure
```
.editorconfig
.git/
.github/
.gitignore
Build.ps1             # PowerShell build script (requires GitVersion, fails in dev)
CONTRIBUTING.md
README.md
global.json           # Specifies .NET 9.0.304 SDK requirement
src/                  # Contains the actual solution and projects
```

### src/ Directory Structure
```
src/
├── Directory.Build.props                                    # Common MSBuild properties
├── Workleap.ComponentModel.DataAnnotations/                 # Main library
│   ├── Workleap.ComponentModel.DataAnnotations.csproj      # Multi-target: net462, net8.0, netstandard2.0
│   ├── PublicAPI.Shipped.txt                               # API compatibility tracking
│   └── *.cs                                                # Data annotation attribute implementations
├── Workleap.ComponentModel.DataAnnotations.Tests/          # Test project
│   ├── Workleap.ComponentModel.DataAnnotations.Tests.csproj # Target: net8.0 (and net462 on Windows)
│   └── *Tests.cs                                           # xUnit test files
└── Workleap.ComponentModel.DataAnnotations.sln             # Solution file
```

### Key Data Annotation Attributes Examples
The library provides the following validated attributes:
- `[Guid]` - Validates GUID format: `[Guid("D")]` for specific format
- `[NotEmpty]` - Validates collections are not empty  
- `[ValidateProperties]` - Recursively validates nested objects
- `[TimeSpan]` - Validates TimeSpan strings: `[TimeSpan("c")]` for specific format
- `[UrlOfKind(UriKind.Absolute)]` - Validates URLs of specific kinds
- `[Contains("text")]` - String contains validation
- `[StartsWith("prefix")]` / `[EndsWith("suffix")]` - String prefix/suffix validation

### Key Project Features
- **Multi-target frameworks**: net462, net8.0, netstandard2.0
- **Strong naming**: Uses assembly signing (.snk file)
- **Public API tracking**: Microsoft.CodeAnalysis.PublicApiAnalyzers
- **Testing**: xUnit framework with 220 tests
- **Coding standards**: Workleap.DotNet.CodingStandards package

### Important Notes
- Always work in the `src/` directory, not the repository root
- Release builds fail due to GitVersion - stick to Debug builds for development
- The library provides custom validation attributes like `[Guid]`, `[NotEmpty]`, `[ValidateProperties]`, etc.
- Tests cover all validation scenarios including edge cases and error messages
- Use `dotnet format` to fix encoding and newline issues automatically

### Error Recovery
If builds fail:
1. Ensure you're in the `src/` directory
2. Try `dotnet clean -c Debug` first
3. Check that .NET 9.0.304 SDK and 8.0.12 runtime are installed
4. Verify PATH includes `$HOME/.dotnet`
5. Never attempt Release builds - they will fail due to GitVersion

### CI/CD Integration
- GitHub Actions workflows exist for CI and publishing
- CI runs the same build sequence but with Release configuration and proper versioning setup
- Always run `dotnet format --verify-no-changes` before committing to avoid CI failures
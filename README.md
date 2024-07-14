# 📄 .NET Template Engine

[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
[![Test](https://github.com/devantler/dotnet-template-engine/actions/workflows/test.yaml/badge.svg)](https://github.com/devantler/dotnet-template-engine/actions/workflows/test.yaml)
[![codecov](https://codecov.io/gh/devantler/dotnet-template-engine/graph/badge.svg?token=RhQPb4fE7z)](https://codecov.io/gh/devantler/dotnet-template-engine)

<details>
  <summary>Show/hide folder structure</summary>

<!-- readme-tree start -->
```
.
├── .github
│   └── workflows
├── Devantler.TemplateEngine
└── Devantler.TemplateEngine.Tests
    ├── GeneratorTests
    └── assets
        └── templates

7 directories
```
<!-- readme-tree end -->

</details>

A simple template engine for .NET.

## 🚀 Getting Started

To get started, you can install the package from NuGet.

```bash
dotnet add package Devantler.TemplateEngine
```

## 📝 Usage

> [!NOTE]
> The template engine uses [Scriban](https://github.com/scriban/scriban) under the hood. So to learn more about the syntax, you can visit the [Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/language.md).

To render a template, you can use the `Generator` class or the `TemplateEngine` class directly.

```csharp
using Devantler.TemplateEngine;

var templateEngine = new TemplateEngine();
var generator = new Generator(templateEngine);

var template = "Hello, {{name}}!"; // or "/path/to/template"
var model = new { Name = "World" };

string resultFromEngine = templateEngine.Render(template, model);
string resultFromGenerator = await generator.GenerateAsync(template, model);

```

You can also generate a file from a template.

```csharp
using Devantler.TemplateEngine;

var generator = new Generator(new TemplateEngine());

var template = "Hello, {{name}}!"; // or "/path/to/template"
var model = new { Name = "World" };
var outputPath = "hello.txt";

await generator.GenerateAsync(outputPath, template, model);
```

Both of these scenarios will render `Hello, World!` as the output, since the `name` property is set to `World`, and the template is `Hello, {{name}}!`.

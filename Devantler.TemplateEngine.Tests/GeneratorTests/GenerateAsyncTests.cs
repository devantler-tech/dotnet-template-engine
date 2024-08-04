namespace Devantler.TemplateEngine.Tests.Unit.GeneratorTests;

/// <summary>
/// Contains unit tests for the <see cref="Generator.GenerateAsync(string, object)"/> and the <see cref="Generator.GenerateAsync(string, string, object, FileMode)"/>
/// </summary>
public class GenerateAsyncTests
{
    Generator Generator { get; } = new Generator(new ScribanTemplateEngine());
    string TemplateContent { get; } = $"Hello, {{{{ name }}}}!{Environment.NewLine}";
  object Model { get; } = new { Name = "World" };
  string ExpectedOutput { get; } = $"Hello, World!{Environment.NewLine}";

  /// <summary>
  /// Tests the <see cref="Generator.GenerateAsync(string, object)"/> method to ensure that it renders the template correctly.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_GivenTemplatePathAndNoFileMode_ShouldRenderTemplate()
  {
    // Arrange
    string templatePath = $"{AppDomain.CurrentDomain.BaseDirectory}/assets/templates/hello-template.txt";

    // Act
    string result = await Generator.GenerateAsync(templatePath, Model);

    // Assert
    Assert.Equal(ExpectedOutput, result);
  }

  /// <summary>
  /// Tests the <see cref="Generator.GenerateAsync(string, string, object, FileMode)"/> method to ensure that it creates a file with the rendered template content.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_GivenTemplatePathAndFileMode_ShouldCreateFileWithRenderedTemplateContent()
  {
    // Arrange
    string outputPath = AppDomain.CurrentDomain.BaseDirectory + "/hello-template-file.txt";
    string templatePath = $"{AppDomain.CurrentDomain.BaseDirectory}/assets/templates/hello-template.txt";

    // Act
    await Generator.GenerateAsync(outputPath, templatePath, Model, FileMode.CreateNew);

    // Assert
    Assert.True(File.Exists(outputPath));
    string fileContent = await File.ReadAllTextAsync(outputPath);
    Assert.Equal(ExpectedOutput, fileContent);

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Tests the <see cref="Generator.GenerateAsync(string, object)"/> method to ensure that it renders the template correctly.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_GivenTemplateContentAndNoFileMode_ShouldRenderTemplate()
  {
    // Act
    string result = await Generator.GenerateAsync(TemplateContent, Model);

    // Assert
    Assert.Equal(ExpectedOutput, result);
  }

  /// <summary>
  /// Tests the <see cref="Generator.GenerateAsync(string, string, object, FileMode)"/> method to ensure that it creates a file with the rendered template content.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_GivenTemplateContentAndFileMode_ShouldCreateFileWithRenderedTemplateContent()
  {
    // Arrange
    string outputPath = AppDomain.CurrentDomain.BaseDirectory + "/hello-template-content.txt";

    // Act
    await Generator.GenerateAsync(outputPath, TemplateContent, Model, FileMode.CreateNew);

    // Assert
    Assert.True(File.Exists(outputPath));
    string fileContent = await File.ReadAllTextAsync(outputPath);
    Assert.Equal(ExpectedOutput, fileContent);

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Tests the <see cref="Generator.GenerateAsync(string, string, object, FileMode)"/> method to ensure that it creates the output directory if it does not exist.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_GivenValidOutputPath_ShouldCreateOutputDirectory()
  {
    // Arrange
    string outputPath = AppDomain.CurrentDomain.BaseDirectory + "/path/to/output/file.txt";

    // Act
    await Generator.GenerateAsync(outputPath, TemplateContent, Model, FileMode.CreateNew);

    // Assert
    Assert.True(File.Exists(outputPath));
    string fileContent = await File.ReadAllTextAsync(outputPath);
    Assert.Equal(ExpectedOutput, fileContent);

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Tests the <see cref="Generator.GenerateAsync(string, string, object, FileMode)"/> method to ensure that it throws an <see cref="ArgumentException"/> when the output path is invalid.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_GivenInvalidOutputPath_ShouldThrowArgumentException()
  {
    // Arrange
    string outputPath = "!@#$%^&*()";

    // Act
    async Task Act() => await Generator.GenerateAsync(outputPath, TemplateContent, Model, FileMode.CreateNew).ConfigureAwait(false);

    // Assert
    _ = await Assert.ThrowsAsync<ArgumentException>(Act);
  }
}

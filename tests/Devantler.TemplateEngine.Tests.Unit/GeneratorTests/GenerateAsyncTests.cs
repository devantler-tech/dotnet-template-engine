namespace Devantler.TemplateEngine.Tests.Unit.GeneratorTests;

/// <summary>
/// Contains unit tests for the <see cref="Generator.GenerateAsync(string, object)"/> and the <see cref="Generator.GenerateAsync(string, string, object, FileMode)"/>
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Tests the <see cref="Generator.GenerateAsync(string, object)"/> method to ensure that it renders the template correctly.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_GivenTemplatePathAndNoFileMode_ShouldRenderTemplate()
  {
    // Arrange
    string templatePath = $"{AppDomain.CurrentDomain.BaseDirectory}/assets/templates/hello-template.txt";
    var model = new { Name = "World" };
    string expectedOutput = "Hello, World!\n";

    var templateEngine = new TemplateEngine();
    var generator = new Generator(templateEngine);

    // Act
    string result = await generator.GenerateAsync(templatePath, model);

    // Assert
    Assert.Equal(expectedOutput, result);
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
    var model = new { Name = "World" };
    string expectedOutput = "Hello, World!\n";

    var templateEngine = new TemplateEngine();
    var generator = new Generator(templateEngine);

    // Act
    await generator.GenerateAsync(outputPath, templatePath, model, FileMode.CreateNew);

    // Assert
    Assert.True(File.Exists(outputPath));
    string fileContent = await File.ReadAllTextAsync(outputPath);
    Assert.Equal(expectedOutput, fileContent);

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Tests the <see cref="Generator.GenerateAsync(string, object)"/> method to ensure that it renders the template correctly.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_GivenTemplateContentAndNoFileMode_ShouldRenderTemplate()
  {
    // Arrange
    string templateContent = "Hello, {{ name }}!\n";
    var model = new { Name = "World" };
    string expectedOutput = "Hello, World!\n";

    var templateEngine = new TemplateEngine();
    var generator = new Generator(templateEngine);

    // Act
    string result = await generator.GenerateAsync(templateContent, model);

    // Assert
    Assert.Equal(expectedOutput, result);
  }

  /// <summary>
  /// Tests the <see cref="Generator.GenerateAsync(string, string, object, FileMode)"/> method to ensure that it creates a file with the rendered template content.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_GivenTemplateContentAndFileMode_ShouldCreateFileWithRenderedTemplateContent()
  {
    // Arrange
    string outputPath = AppDomain.CurrentDomain.BaseDirectory + "/hello-template-content.txt";
    string templateContent = "Hello, {{ name }}!\n";
    var model = new { Name = "World" };
    string expectedOutput = "Hello, World!\n";

    var templateEngine = new TemplateEngine();
    var generator = new Generator(templateEngine);

    // Act
    await generator.GenerateAsync(outputPath, templateContent, model, FileMode.CreateNew);

    // Assert
    Assert.True(File.Exists(outputPath));
    string fileContent = await File.ReadAllTextAsync(outputPath);
    Assert.Equal(expectedOutput, fileContent);

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
    string templateContent = "Hello, {{ name }}!\n";
    var model = new { Name = "World" };
    string expectedOutput = "Hello, World!\n";

    var templateEngine = new TemplateEngine();
    var generator = new Generator(templateEngine);

    // Act
    await generator.GenerateAsync(outputPath, templateContent, model, FileMode.CreateNew);

    // Assert
    Assert.True(File.Exists(outputPath));
    string fileContent = await File.ReadAllTextAsync(outputPath);
    Assert.Equal(expectedOutput, fileContent);

    // Cleanup
    File.Delete(outputPath);
  }
}

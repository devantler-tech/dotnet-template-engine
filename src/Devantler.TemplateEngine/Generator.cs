using System.Text;

namespace Devantler.TemplateEngine;

/// <inheritdoc />
public class Generator(ITemplateEngine templateEngine) : IGenerator
{
  readonly ITemplateEngine _templateEngine = templateEngine;

  /// <inheritdoc />
  public Task<string> GenerateAsync(string templateContentOrPath, object model) =>
    File.Exists(templateContentOrPath) ?
      _templateEngine.RenderFromPathAsync(templateContentOrPath, model) :
      _templateEngine.RenderFromContentAsync(templateContentOrPath, model);

  /// <inheritdoc />
  public async Task GenerateAsync(
    string outputPath,
    string templateContentOrPath,
    object model,
    FileMode fileMode = FileMode.CreateNew
  )
  {
    string? directoryName = Path.GetDirectoryName(outputPath) ?? throw new ArgumentNullException(nameof(outputPath), "The output path is invalid.");
    if (!Directory.Exists(directoryName))
      _ = Directory.CreateDirectory(directoryName);

    var fileStream = new FileStream(outputPath, fileMode, FileAccess.Write);
    string renderedTemplate = File.Exists(templateContentOrPath) ?
      await _templateEngine.RenderFromPathAsync(templateContentOrPath, model) :
      await _templateEngine.RenderFromContentAsync(templateContentOrPath, model);
    await fileStream.WriteAsync(Encoding.UTF8.GetBytes(renderedTemplate));
    await fileStream.FlushAsync();
    fileStream.Close();
  }
}

using System.Text;

namespace Devantler.Commons.TemplateEngine;

/// <inheritdoc />
public class Generator(ITemplateEngine templateEngine) : IGenerator
{
  readonly ITemplateEngine _templateEngine = templateEngine;

  /// <inheritdoc />
  public Task<string> GenerateAsync(string templatePath, object model) =>
    _templateEngine.RenderFromPathAsync(templatePath, model);

  /// <inheritdoc />
  public async Task GenerateAsync(
    string outputPath,
    string templatePath,
    object model,
    FileMode fileMode = FileMode.CreateNew
  )
  {
    string directoryName = Path.GetDirectoryName(outputPath) ?? throw new ArgumentNullException(nameof(outputPath));
    if (!Directory.Exists(directoryName))
      _ = Directory.CreateDirectory(directoryName);

    var fileStream = new FileStream(outputPath, fileMode, FileAccess.Write);
    await fileStream.WriteAsync(Encoding.UTF8.GetBytes(await _templateEngine.RenderFromPathAsync(templatePath, model)));
    await fileStream.FlushAsync();
    fileStream.Close();
  }
}

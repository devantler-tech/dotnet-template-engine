using System.Text;

namespace DevantlerTech.TemplateEngine;

/// <inheritdoc />
public class Generator(ITemplateEngine templateEngine) : IGenerator
{
  readonly ITemplateEngine _templateEngine = templateEngine;

  /// <inheritdoc />
  public Task<string> GenerateAsync(string templateContentOrPath, object model) =>
   _templateEngine.RenderAsync(templateContentOrPath, model);

  /// <inheritdoc />
  public async Task GenerateAsync(
    string templateContentOrPath,
    string outputPath,
    object model,
    FileMode fileMode = FileMode.CreateNew
  )
  {
    string? directoryName = Path.GetDirectoryName(outputPath);
    if (string.IsNullOrEmpty(directoryName))
      throw new ArgumentException("The output path is invalid.", nameof(outputPath));
    if (!Directory.Exists(directoryName))
      _ = Directory.CreateDirectory(directoryName);

    var fileStream = new FileStream(outputPath, fileMode, FileAccess.Write);
    string renderedTemplate = await _templateEngine.RenderAsync(templateContentOrPath, model).ConfigureAwait(false);
    await fileStream.WriteAsync(Encoding.UTF8.GetBytes(renderedTemplate)).ConfigureAwait(false);
    await fileStream.FlushAsync().ConfigureAwait(false);
    fileStream.Close();
  }
}

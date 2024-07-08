namespace Devantler.TemplateEngine;


/// <summary>
/// Represents a template engine that can render templates from a file path or from content.
/// </summary>
public class TemplateEngine : ITemplateEngine
{
  /// <summary>
  /// Renders a template from a file path.
  /// </summary>
  /// <param name="templateContentOrPath">The content of the template or the path to the template file.</param>
  /// <param name="model">The model object to use for rendering the template.</param>
  /// <returns>A task that represents the asynchronous rendering operation. The task result contains the rendered template as a string.</returns>
  public async Task<string> RenderAsync(string templateContentOrPath, object model)
  {
    string templateContent = File.Exists(templateContentOrPath) ?
      await File.ReadAllTextAsync(templateContentOrPath) :
      templateContentOrPath;
    var parsedTemplate = Scriban.Template.Parse(templateContent);
    return await parsedTemplate.RenderAsync(model);
  }
}

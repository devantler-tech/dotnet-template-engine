namespace DevantlerTech.TemplateEngine;

/// <summary>
/// Represents a template engine that can render templates using a specified model.
/// </summary>
public interface ITemplateEngine
{
  /// <summary>
  /// Renders a template from the specified file path using the provided model.
  /// </summary>
  /// <param name="templateContentOrPath">The content of the template or the path to the template file.</param>
  /// <param name="model">The model object to use for rendering the template.</param>
  /// <returns>A task that represents the asynchronous rendering operation. The task result contains the rendered template as a string.</returns>
  Task<string> RenderAsync(string templateContentOrPath, object model);
}

using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Sitecore.Remote.Installation.Client.Response
{
  /// <summary>
  /// Html page response
  /// </summary>
  public class HtmlPage
  {
    /// <summary>
    /// The get value by name in hidden pattern
    /// </summary>
    public static readonly string GetValueByNameInInputPattern = "<input.*?type=\".*?\".*?name=\"{0}\".*?value=\"(.*?)\".*?\\/>";

    /// <summary>
    /// The get name by element type pattern
    /// </summary>
    public static readonly string GetNameByInputTypePattern = "<input.*?type=\"{0}\".*?name=\"(.*?)\".*?\\/>";

    /// <summary>
    /// Gets the content.
    /// </summary>
    /// <value>
    /// The content.
    /// </value>
    public string Content { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="HtmlPage"/> class.
    /// </summary>
    /// <param name="content">The content.</param>
    public HtmlPage(string content)
    {
      this.Content = content;
    }

    /// <summary>
    /// Gets the by regex.
    /// </summary>
    /// <param name="pattern">The pattern.</param>
    /// <param name="param">The parameter.</param>
    /// <returns></returns>
    private string GetByRegex(string pattern, string param)
    {
      var match = new Regex(string.Format(pattern, param), RegexOptions.IgnoreCase | RegexOptions.Multiline).Match(this.Content);

      if (match.Success)
      {
        return match.Groups.OfType<Group>().LastOrDefault()?.Value ?? string.Empty;
      }

      return string.Empty;
    }

    /// <summary>
    /// Gets the name of the value by element.
    /// </summary>
    /// <param name="elementName">Name of the element.</param>
    /// <returns></returns>
    public string GetValueByElementName(string elementName)
    {
      return HttpUtility.UrlEncode(this.GetByRegex(GetValueByNameInInputPattern, elementName));
  }

    /// <summary>
    /// Gets the type of the name by element.
    /// </summary>
    /// <param name="inputType">Name of the element.</param>
    /// <returns></returns>
    public string GetNameByElementType(string inputType)
    {
      return this.GetByRegex(GetNameByInputTypePattern, inputType);
    }

    /// <summary>
    /// Gets the by regex.
    /// </summary>
    /// <param name="pattern">The pattern.</param>
    /// <returns></returns>
    public string GetByRegex(string pattern)
    {
      return string.Empty;
    }
  }
}

using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Sitecore.Remote.Installation.Client.Responses
{
  /// <summary>
  /// Json response
  /// </summary>
  public class JsonResponse
  {
    /// <summary>
    ///   Initializes a new instance of the <see cref="HtmlResponse" /> class.
    /// </summary>
    /// <param name="content">The content.</param>
    public JsonResponse(string content)
    {
      this.Content = content;
      this.CommandsResponse = CommandsResponse.Empty;

      if (!string.IsNullOrEmpty(this.Content))
      {
        using (var stream = new MemoryStream(Encoding.Default.GetBytes(this.Content)))
        {
          var serializer = new DataContractJsonSerializer(typeof(CommandsResponse));

          this.CommandsResponse = serializer.ReadObject(stream) as CommandsResponse;
        }
      }
    }

    /// <summary>
    ///   Gets the content.
    /// </summary>
    /// <value>
    ///   The content.
    /// </value>
    public string Content { get; }

    /// <summary>
    /// Gets the commands response.
    /// </summary>
    /// <value>
    /// The commands response.
    /// </value>
    public CommandsResponse CommandsResponse { get; }

    /// <summary>
    /// Gets the atribute value.
    /// </summary>
    public string GetAtributeValue(string id)
    {
      return this.CommandsResponse?.Commands.FirstOrDefault(c => c.CommandName == "SetAttribute" && c.Id == id)?.Value;
    }
  }
}
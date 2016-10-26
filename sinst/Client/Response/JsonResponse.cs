using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Sitecore.Remote.Installation.Client.Response
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
    }

    /// <summary>
    ///   Gets the content.
    /// </summary>
    /// <value>
    ///   The content.
    /// </value>
    public string Content { get; }

    /// <summary>
    /// Gets the atribute value.
    /// </summary>
    public string GetAtributeValue(string id)
    {      
      using (var stream = new MemoryStream(Encoding.Default.GetBytes(this.Content)))
      {
        var serializer = new DataContractJsonSerializer(typeof(CommandsResponse));

        var commandsResponse = serializer.ReadObject(stream) as CommandsResponse;

        return commandsResponse?.Commands.FirstOrDefault(c => c.CommandName == "SetAttribute" && c.Id == id)?.Value;
      }
    }

    /// <summary>
    /// Commands response
    /// </summary>
    [DataContract]
    private class CommandsResponse
    {
      /// <summary>
      /// Gets or sets the commands.
      /// </summary>
      /// <value>
      /// The commands.
      /// </value>
      [DataMember(Name = "commands")]
      public Command[] Commands { get; set; }
    }

    /// <summary>
    /// Command
    /// </summary>
    [DataContract]
    private class Command
    {
      /// <summary>
      /// Gets or sets the name of the command.
      /// </summary>
      /// <value>
      /// The name of the command.
      /// </value>
      [DataMember(Name = "command")]
      public string CommandName { get; set; }

      /// <summary>
      /// Gets or sets the value.
      /// </summary>
      /// <value>
      /// The value.
      /// </value>
      [DataMember(Name = "value")]
      public string Value { get; set; }

      /// <summary>
      /// Gets or sets the identifier.
      /// </summary>
      /// <value>
      /// The identifier.
      /// </value>
      [DataMember(Name = "id")]
      public string Id { get; set; }

      /// <summary>
      /// Gets or sets the name.
      /// </summary>
      /// <value>
      /// The name.
      /// </value>
      [DataMember(Name = "name")]
      public string Name { get; set; }
    }
  }
}
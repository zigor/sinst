using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Sitecore.Remote.Installation.Client.Responses
{
  /// <summary>
  /// Commands response
  /// </summary>
  [DataContract]
  public class CommandsResponse
  {
    /// <summary>
    /// The empty
    /// </summary>
    public static readonly CommandsResponse Empty = new CommandsResponse { Commands = Enumerable.Empty<Command>() };

    /// <summary>
    /// Gets or sets the commands.
    /// </summary>
    /// <value>
    /// The commands.
    /// </value>
    [DataMember(Name = "commands")]
    public IEnumerable<Command> Commands { get; set; }

    /// <summary>
    /// Finds the name of the by.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    public Command FindByName(string name)
    {
      return this.Commands.FirstOrDefault(c => c.CommandName == name);
    }

    /// <summary>
    /// Command
    /// </summary>
    [DataContract]
    public class Command
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
using System;

namespace Sitecore.Remote.Installation.Installer.Events
{
  /// <summary>
  ///   Output required event args
  /// </summary>
  /// <seealso cref="System.EventArgs" />
  public class OutputRequredEventArgs : EventArgs
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="OutputRequredEventArgs" /> class.
    /// </summary>
    /// <param name="message">The message.</param>
    public OutputRequredEventArgs(string message) : this(message, MessageLevel.Details)
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OutputRequredEventArgs"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="level">The level.</param>
    public OutputRequredEventArgs(string message, MessageLevel level)
    {
      this.Message = message;
      this.Level = level;
    }

    /// <summary>
    ///   Gets the message.
    /// </summary>
    /// <value>
    ///   The message.
    /// </value>
    public string Message { get; }

    /// <summary>
    ///   Gets or sets the level.
    /// </summary>
    /// <value>
    ///   The level.
    /// </value>
    public MessageLevel Level { get; }
  }
}
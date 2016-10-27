using System.Collections.Specialized;

namespace Sitecore.Remote.Installation.Installer.Events
{
  /// <summary>
  /// Input required event args
  /// </summary>
  /// <seealso cref="System.EventArgs" />
  public class InputRequiredEventArgs : OutputRequredEventArgs
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="InputRequiredEventArgs" /> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="choices">The choices.</param>
    public InputRequiredEventArgs(string message, NameValueCollection choices) : base(message, MessageLevel.Details)
    {
      this.Choices = choices;
    }
    
    /// <summary>
    /// Gets or sets the choise.
    /// </summary>
    /// <value>
    /// The choise.
    /// </value>
    public NameValueCollection Choices { get; }

    /// <summary>
    /// Gets or sets the results.
    /// </summary>
    /// <value>
    /// The results.
    /// </value>
    public string Result { get; set; }
  }
}
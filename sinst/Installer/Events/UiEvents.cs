using System;
using System.Collections.Specialized;

namespace Sitecore.Remote.Installation.Installer.Events
{
  /// <summary>
  /// Ui installer events
  /// </summary>
  public class UiEvents
  {
    /// <summary>
    ///   The input
    /// </summary>
    public EventHandler<InputRequiredEventArgs> Input;

    /// <summary>
    /// The output
    /// </summary>
    public EventHandler<OutputRequredEventArgs> Output;

    /// <summary>
    /// Raises the output required.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="level">The level.</param>
    public void RaiseOutputRequired(string message, MessageLevel level = MessageLevel.Details)
    {
      var handler = this.Output;
      handler?.Invoke(this, new OutputRequredEventArgs(message, level));
    }

    /// <summary>
    /// Raises the output required.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="choices">The choices.</param>
    /// <returns></returns>
    public string RaiseInputRequired(string message, NameValueCollection choices)
    {
      var handler = this.Input;

      var args = new InputRequiredEventArgs(message, choices);
      handler?.Invoke(this, args);

      return args.Result;
    }
  }
}
using System;
using System.Collections.Specialized;
using Sitecore.Remote.Installation.Installer.Events;

namespace Sitecore.Remote.Installation.Installer
{
  /// <summary>
  /// Ui installer events
  /// </summary>
  public class UiInstallerEvents
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
    public void RaiseOutputRequired(string message, MessageLevel level)
    {
      var handler = this.Output;
      handler?.Invoke(this, new OutputRequredEventArgs(message, level));
    }

    /// <summary>
    /// Raises the output required.
    /// </summary>
    /// <param name="choices">The choices.</param>
    /// <returns></returns>
    public string RaiseInputRequired(NameValueCollection choices)
    {
      var handler = this.Input;

      var args = new InputRequiredEventArgs(choices);
      handler?.Invoke(this, args);

      return args.Result;
    }
  }
}
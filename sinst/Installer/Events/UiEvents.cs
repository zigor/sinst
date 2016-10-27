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
    /// The start
    /// </summary>
    public EventHandler Start;

    /// <summary>
    /// The complete
    /// </summary>
    public EventHandler Success;

    /// <summary>
    /// The failure
    /// </summary>
    public EventHandler Failure;

    /// <summary>
    ///   The input
    /// </summary>
    public EventHandler<InputRequiredEventArgs> Input;

    /// <summary>
    /// The output
    /// </summary>
    public EventHandler<OutputRequredEventArgs> Output;

    /// <summary>
    /// Raises the start.
    /// </summary>
    public void RaiseStart()
    {
      var handler = this.Start;
      handler?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Raises the success.
    /// </summary>
    public void RaiseSuccess()
    {
      var handler = this.Success;
      handler?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Raises the failure.
    /// </summary>
    public void RaiseFailure()
    {
      var handler = this.Failure;
      handler?.Invoke(this, EventArgs.Empty);
    }

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
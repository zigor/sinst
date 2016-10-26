using System.Collections.Specialized;
using Sitecore.Remote.Installation.Attributes;
using Sitecore.Remote.Installation.Diagnostic;
using Sitecore.Remote.Installation.Installer;
using Sitecore.Remote.Installation.Installer.Events;
using Sitecore.Remote.Installation.Models;
using Sitecore.Remote.Installation.Pipelines.Metadata;

namespace Sitecore.Remote.Installation.Pipelines
{
  /// <summary>
  /// Override security command 
  /// </summary>
  [Pipeline(PipelineNames.InstallationConflicts), Processor(30)]
  public class OverrideSecurityCommand
  {
    /// <summary>
    /// The default choises
    /// </summary>
    private static readonly NameValueCollection defaultChoices;

    /// <summary>
    /// Initializes the <see cref="OverrideSecurityCommand"/> class.
    /// </summary>
    static OverrideSecurityCommand()
    {
      defaultChoices = new NameValueCollection
      {
        {"Continue", "continue"},
        {"Always", "always"},
        {"Abort", "abort"}
      };
    }

    /// <summary>
    /// Processes the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    public void Process(IPipelineContext<InstallationConflictDetails> context)
    {
      Assert.ArgumentNotNull(context, nameof(context));

      var command = context.Model.CommandsResponse.FindByName("ShowModalDialog");
      if (command?.Value?.Contains("xmlcontrol=ContinueAlwaysAbort") != true)
      {
        return;
      }

      var message = System.Web.HttpUtility.ParseQueryString(command.Value)["te"];
      UiInstaller.Instance.Events.RaiseOutputRequired(message, MessageLevel.Details);

      var choice = UiInstaller.Instance.Events.RaiseInputRequired(new NameValueCollection(defaultChoices));

      context.Model.Result = choice;
      context.Model.RemotePipelineId = context.Model.CommandsResponse.FindByName("SetPipeline")?.Value;
    }
  }
}

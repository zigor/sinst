using System.Collections.Specialized;
using System.Security.Policy;
using Sitecore.Remote.Installation.Attributes;
using Sitecore.Remote.Installation.Diagnostic;
using Sitecore.Remote.Installation.Installer;
using Sitecore.Remote.Installation.Installer.Events;
using Sitecore.Remote.Installation.Models;
using Sitecore.Remote.Installation.Pipelines.Metadata;

namespace Sitecore.Remote.Installation.Pipelines
{
  /// <summary>
  /// Override Fiels command
  /// </summary>
  [Pipeline(PipelineNames.InstallationConflicts), Processor(10)]
  public class OverrideFilesCommand
  {
    /// <summary>
    /// The default choices
    /// </summary>
    private static readonly NameValueCollection defaultChoices;

    /// <summary>
    /// Initializes the <see cref="OverrideFilesCommand"/> class.
    /// </summary>
    static OverrideFilesCommand()
    {
      defaultChoices = new NameValueCollection
      {
        {"Yes", "yes"},
        {"Yes to All", "yes to all"},
        {"No", "no"},
        {"No to All", "no to all"},
        {"Cancel", "cancel"}
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
      if (command?.Value?.Contains("xmlcontrol=YesNoCancelAll") != true)
      {
        return;
      }

      var message = System.Web.HttpUtility.ParseQueryString(command.Value)["te"];
      UiInstaller.Instance.Events.RaiseOutputRequired(message, MessageLevel.Details);

      context.Model.Result = UiInstaller.Instance.Events.RaiseInputRequired(new NameValueCollection(defaultChoices));

      context.Model.RemotePipelineId = context.Model.CommandsResponse.FindByName("SetPipeline")?.Value;
    }
  }
}

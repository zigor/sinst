using System.Collections.Specialized;
using System.Web;
using Sitecore.Remote.Installation.Attributes;
using Sitecore.Remote.Installation.Diagnostic;
using Sitecore.Remote.Installation.Installer;
using Sitecore.Remote.Installation.Models;
using Sitecore.Remote.Installation.Pipelines.Metadata;

namespace Sitecore.Remote.Installation.Pipelines
{
  /// <summary>
  ///   Override Fiels command
  /// </summary>
  [Pipeline(PipelineNames.InstallationConflicts)]
  [Processor(10)]
  public class OverrideFilesCommand : InstallerCommandProcessor
  {
    /// <summary>
    ///   The default choices
    /// </summary>
    private static readonly NameValueCollection defaultChoices;

    /// <summary>
    ///   Initializes the <see cref="OverrideFilesCommand" /> class.
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
    ///   Initializes a new installer of the <see cref="OverrideFilesCommand" /> class.
    /// </summary>
    public OverrideFilesCommand() : this(UiInstaller.Instance)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="OverrideFilesCommand" /> class.
    /// </summary>
    /// <param name="installer">The installer.</param>
    public OverrideFilesCommand(UiInstaller installer) : base(installer)
    {
    }

    /// <summary>
    ///   Processes the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    public override void Process(IPipelineContext<InstallationConflictDetails> context)
    {
      Assert.ArgumentNotNull(context, nameof(context));

      var command = context.Model.CommandsResponse.FindByName("ShowModalDialog");
      if (command?.Value?.Contains("xmlcontrol=YesNoCancelAll") != true)
      {
        return;
      }

      var message = HttpUtility.ParseQueryString(command.Value)["te"];

      context.Model.Result = this.Installer.Events.RaiseInputRequired(message, new NameValueCollection(defaultChoices));
      context.Model.RemotePipelineId = context.Model.CommandsResponse.FindByName("SetPipeline")?.Value;
    }
  }
}
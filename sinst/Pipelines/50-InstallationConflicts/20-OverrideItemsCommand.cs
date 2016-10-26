using System;
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
  /// Override items conflict
  /// </summary>
  [Pipeline(PipelineNames.InstallationConflicts), Processor(20)]
  public class OverrideItemsConflict
  {
    /// <summary>
    /// The default choices
    /// </summary>
    private static readonly NameValueCollection defaultChoices;

    static OverrideItemsConflict()
    {
      defaultChoices = new NameValueCollection
      {
        {"Override", "1|1|False"},
        {"Override & Apply to All", "1|1|True"},
        {"Merge -> Clear", "2|1|False"},
        {"Merge -> Clear & Apply to All", "2|1|True"},
        {"Merge -> Append", "2|2|False"},
        {"Merge -> Append & Apply to All", "2|2|True"},
        {"Merge -> Merge", "2|3|False"},
        {"Merge -> Merge & Apply to All", "2|3|True"},
        {"Side By Side", "4|1|False"},
        {"Side By Side & Apply to All", "4|1|True"},
        {"Skip", "3|2|False"},
        {"Skip & Apply to All", "3|2|True"},
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
      if (command?.Value?.Contains("xmlcontrol=Installer.GetPasteMode") != true)
      {
        return;
      }

      var message = "Item being installed already exists in database: " + System.Web.HttpUtility.ParseQueryString(command.Value)["ph"];
     

      UiInstaller.Instance.Events.RaiseOutputRequired(message, MessageLevel.Details);

      context.Model.Result = UiInstaller.Instance.Events.RaiseInputRequired(new NameValueCollection(defaultChoices));
      context.Model.RemotePipelineId = context.Model.CommandsResponse.FindByName("SetPipeline")?.Value;
    }
  }
}

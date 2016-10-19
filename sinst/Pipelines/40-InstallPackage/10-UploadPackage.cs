using System.Collections.Specialized;
using Sitecore.Remote.Installation.Attributes;
using Sitecore.Remote.Installation.Client.Response;
using Sitecore.Remote.Installation.Models;
using Sitecore.Remote.Installation.Pipelines.Metadata;

namespace Sitecore.Remote.Installation.Pipelines
{
  /// <summary>
  /// Uploads package
  /// </summary>
  [Pipeline(PipelineNames.InstallPackage), Processor(10)]
  public class UploadPackage
  {
    /// <summary>
    /// Processes the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    public void Process(IPipelineContext<InstallPackageDetails> context)
    {
      var uploadPageContent = context.Model.Client.Get("/sitecore/shell/applications/install/dialogs/Upload Package/UploadPackage2.aspx");

      if (string.IsNullOrEmpty(uploadPageContent))
      {
        context.Aborted = true;
        return;
      }

      var uploadPackagePage = new HtmlPage(uploadPageContent);

      var viewState = uploadPackagePage.GetValueByElementName("__VIEWSTATE", false);
      var csrfToken = uploadPackagePage.GetValueByElementName("__CSRFTOKEN", false);
      var fileHandler = uploadPackagePage.GetNameByElementType("file");
      var data = new NameValueCollection
      {
        { "__CSRFTOKEN", csrfToken },
        { "__VIEWSTATE", viewState },
        { "Unzip", "0" },
        { "Overwrite", "1" },
        { "__PARAMETERS", "" },
        { "__EVENTTARGET", "NextButton" },
        { "__EVENTARGUMENT", "" },
        { "__SOURCE", "NextButton" },
        { "__EVENTTYPE", "click" },
        { "__ISEVENT", "1" },
        { "__BUTTON", "0" },
        { fileHandler, context.Model.Package }
      };

      var content = context.Model.Client.Post("/sitecore/shell/applications/install/dialogs/Upload Package/UploadPackage2.aspx", data);

      //content = context.Model.Client.Post("/sitecore/shell/applications/install/dialogs/Upload Package/UploadPackage2.aspx", data);

      //{ "command":"SetAttribute","value":"C78176D9D1A648CDABF5AC090D732A82","id":"Path","name":"value"},

      new HttpFileUploadClient(context.Model.Client)
        .Post("sitecore/shell/applications/install/dialogs/Upload%20Package/UploadPackage2.aspx", fileHandler,
        context.Model.Package, "application/zip, application/octet-stream, application/x-zip-compressed", data);
    }

  }
}

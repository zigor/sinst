using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sitecore.Remote.Installation.Client.Responses;
using Sitecore.Remote.Installation.Diagnostic;
using Sitecore.Remote.Installation.Models;
using Sitecore.Remote.Installation.Pipelines;

namespace Sitecore.Remote.Installation.Client.Requests
{
  /// <summary>
  /// Installation package wizard request
  /// </summary>
  public class InstallationPackageWizardRequest
  {
    /// <summary>
    /// The installation wizard URL
    /// </summary>
    private static readonly string InstallationWizardUrl =
      "sitecore/shell/Applications/Tools/Installer/InstallationWizard";

    /// <summary>
    /// The form data
    /// </summary>
    private NameValueCollection formData;

    /// <summary>
    /// Gets or sets the client.
    /// </summary>
    /// <value>
    /// The client.
    /// </value>
    public IHttpClient Client { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="InstallationPackageWizardRequest"/> class.
    /// </summary>
    /// <param name="client">The client.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    public InstallationPackageWizardRequest(IHttpClient client)
    {
      this.Client = client;
    }

    /// <summary>
    /// Installs the specified package name.
    /// </summary>
    /// <param name="packageName">Name of the package.</param>
    /// <returns></returns>
    public void Install(string packageName)
    {
      var content = this.Open();
      this.SelectFile(content, packageName);
      this.SelectLicense();
      this.Confirm();

      Task.Run(this.Monitor).Wait();
    }

    /// <summary>
    /// Opens this instance.
    /// </summary>
    /// <returns></returns>
    private string Open()
    {
      var content = this.Client.Get(InstallationWizardUrl);
      return content;
    }

    /// <summary>
    /// Selects the file.
    /// </summary>
    /// <param name="pageContent">Content of the page.</param>
    /// <param name="packageName">Name of the package.</param>
    private void SelectFile(string pageContent, string packageName)
    {
      Assert.ArgumentNotNullOrEmpty(pageContent, nameof(pageContent));
      Assert.ArgumentNotNullOrEmpty(packageName, nameof(packageName));

      var package = Path.GetFileName(packageName);
      var uploadPackagePage = new HtmlResponse(pageContent);

      var viewState = uploadPackagePage.GetValueByElementName("__VIEWSTATE", false);
      var csrfToken = uploadPackagePage.GetValueByElementName("__CSRFTOKEN", false);

      this.formData = new NameValueCollection
      {
        {"__PARAMETERS", ""},
        {"__EVENTTARGET", "NextButton"},
        {"__EVENTARGUMENT", ""},
        {"__SOURCE", "NextButton"},
        {"__CSRFTOKEN", csrfToken},
        {"__VIEWSTATE", viewState},
        {"__EVENTTYPE", "click"},
        {"__ISEVENT", "1"},
        {"PackageFile", package},
        {"AcceptLicense", "yes"},
        {"PackageName", package},
        {"Version", "1.0"},
        {"Author", ""},
        {"Publisher", ""},
        {"Restart", "1"},
        {"ReadmeText", ""}
      };

      this.Client.Post(InstallationWizardUrl, this.formData);
    }

    /// <summary>
    /// Selects the license.
    /// </summary>
    private void SelectLicense()
    {
      this.Client.Post(InstallationWizardUrl, this.formData);
    }

    /// <summary>
    /// Confirms this instance.
    /// </summary>
    private void Confirm()
    {
      this.Client.Post(InstallationWizardUrl, this.formData);

      this.formData["__PARAMETERS"] = "taskmonitor:check";
      this.formData["__EVENTTARGET"] = string.Empty;
      this.formData["__SOURCE"] = string.Empty;
    }

    /// <summary>
    /// Monitors this instance.
    /// </summary>
    private async Task Monitor()
    {
      string response = null;
      do
      {
        response = this.Client.Post(InstallationWizardUrl, this.formData);

        var result = InstallationConflictsPipeline.Run(this.Client, new JsonResponse(response).CommandsResponse).Result;

        if (result.Count > 0)
        {
          var resultFormData = new NameValueCollection(this.formData);
          resultFormData.Add(result);
          resultFormData.Remove("__PARAMETERS");
          resultFormData.Remove("__EVENTTARGET");
          resultFormData.Remove("__EVENTARGUMENT");
          resultFormData.Remove("__SOURCE");
          resultFormData.Remove("__EVENTTYPE");
          resultFormData.Remove("__ISEVENT");

          this.Client.Post(InstallationWizardUrl, resultFormData);
        }

        Console.Write(response);

        await Task.Delay(2000);

      } while (!string.IsNullOrEmpty(response));
    }
  }
}
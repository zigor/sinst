using System.Collections.Specialized;
using System.Linq;
using Sitecore.Remote.Installation.Client.Response;
using Sitecore.Remote.Installation.Diagnostic;
using Sitecore.Remote.Installation.Models;

namespace Sitecore.Remote.Installation.Client
{
  /// <summary>
  /// Upload package wizard request
  /// </summary>
  public class UploadPackageWizardRequest
  {
    /// <summary>
    /// The upload package wizard URL
    /// </summary>
    private static readonly string uploadPackageWizardUrl = "sitecore/shell/applications/install/dialogs/Upload%20Package/UploadPackage2.aspx";

    /// <summary>
    /// The attachment content type
    /// </summary>
    private static readonly string attachmentContentType = "application/x-zip-compressed";

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
    protected IHttpClient Client { get; }

    /// <summary>
    /// Gets or sets the package path.
    /// </summary>
    /// <value>
    /// The package path.
    /// </value>
    public string PackagePath { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UploadPackageWizardRequest" /> class.
    /// </summary>
    /// <param name="client">The client.</param>
    public UploadPackageWizardRequest(IHttpClient client)
    {
      this.Client = client;
      this.formData = new NameValueCollection();
    }

    /// <summary>
    /// Uploads the specified path.
    /// </summary>
    /// <param name="packagePath">The package path.</param>
    /// <returns></returns>
    public virtual bool Upload(string packagePath)
    {
      var pageContent = this.Open();

      if (string.IsNullOrEmpty(pageContent))
      {        
        return false;
      }

      this.AddFile(pageContent, packagePath);
      this.Confirm();
      this.PostFile(packagePath);

      return true;
    }

    /// <summary>
    /// Opens this instance.
    /// </summary>
    /// <returns></returns>
    protected virtual string Open()
    {
      var uploadPageContent = this.Client.Get(uploadPackageWizardUrl);
      return uploadPageContent;
    }

    /// <summary>
    /// Adds the file.
    /// </summary>
    /// <param name="pageContent">Content of the page.</param>
    /// <param name="packagePath">The package path.</param>
    /// <returns></returns>
    private void AddFile(string pageContent, string packagePath)
    {
      Assert.ArgumentNotNullOrEmpty(pageContent, nameof(pageContent));
      Assert.ArgumentNotNullOrEmpty(packagePath, nameof(packagePath));

      var uploadPackagePage = new HtmlResponse(pageContent);

      var viewState = uploadPackagePage.GetValueByElementName("__VIEWSTATE", false);
      var csrfToken = uploadPackagePage.GetValueByElementName("__CSRFTOKEN", false);
      var fileHandler = uploadPackagePage.GetNameByElementType("file");
      this.formData = new NameValueCollection
      {
        { "__CSRFTOKEN", csrfToken },
        { "__VIEWSTATE", viewState },
        { "Unzip", "0" },
        { "Overwrite", "1" },
        { "__EVENTTARGET", "NextButton" },
        { "__EVENTARGUMENT", "" },
        { "__SOURCE", "NextButton" },
        { "__EVENTTYPE", "click" },
        { "__ISEVENT", "1" },
        { "__BUTTON", "0" },
        { fileHandler, packagePath }
      };

      this.Client.Post(uploadPackageWizardUrl, this.formData);
    }

    /// <summary>
    /// Confirms this instance.
    /// </summary>
    private void Confirm()
    {
      var content = this.Client.Post(uploadPackageWizardUrl, this.formData);

      this.formData.Add("Path", new JsonResponse(content).GetAtributeValue("Path"));
    }

    /// <summary>
    /// Posts the file.
    /// </summary>
    private void PostFile(string packagePath)
    {
      var fileHandler = this.formData.AllKeys.FirstOrDefault(k => this.formData[k] == packagePath);
      this.formData.Remove(fileHandler);
      this.formData.Remove("__EVENTTARGET");
      this.formData.Remove("__EVENTARGUMENT");
      this.formData.Remove("__SOURCE");
      this.formData.Remove("__EVENTTYPE");
      this.formData.Remove("__ISEVENT");
      this.formData.Remove("__BUTTON");

      new HttpFileUploadClient(this.Client)
        .Post(uploadPackageWizardUrl, fileHandler, packagePath, attachmentContentType, this.formData);
    } 
  }
}

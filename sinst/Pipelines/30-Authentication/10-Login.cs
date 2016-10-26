using System.Collections.Specialized;
using Sitecore.Remote.Installation.Attributes;
using Sitecore.Remote.Installation.Client.Responses;
using Sitecore.Remote.Installation.Models;
using Sitecore.Remote.Installation.Pipelines.Metadata;

namespace Sitecore.Remote.Installation.Pipelines
{
  /// <summary>
  /// Read login page
  /// </summary>
  [Pipeline(PipelineNames.Authentication), Processor(10)]
  public class Login
  {
    /// <summary>
    /// Processes the specified client.
    /// </summary>
    /// <param name="pipelineContext">The args.</param>
    public void Process(IPipelineContext<IHttpClient> pipelineContext)
    {
      var client = pipelineContext.Model;

      var loginContent = client.Get("sitecore/login");

      if (string.IsNullOrEmpty(loginContent))
      {
        pipelineContext.Aborted = true;
        return;
      }

      var loginPage = new HtmlResponse(loginContent);

      var viewState = loginPage.GetValueByElementName("__VIEWSTATE");
      var submit = loginPage.GetNameByElementType("submit");
      var submitValue = loginPage.GetValueByElementName(submit);
      var viewStateGenerator = loginPage.GetValueByElementName("__VIEWSTATEGENERATOR");
      var eventValidation = loginPage.GetValueByElementName("__EVENTVALIDATION");

      var data = new NameValueCollection
      {
        { "__EVENTTARGET", "" },
        { "__EVENTARGUMENT", ""},
        { "__VIEWSTATE", viewState },
        { "__VIEWSTATEGENERATOR", viewStateGenerator },
        { "__EVENTVALIDATION", eventValidation },
        { "UserName", pipelineContext.Model.Connection.Credentials.UserName },
        { "Password", pipelineContext.Model.Connection.Credentials.Password },
        { submit, submitValue }
      };

      client.Post("sitecore/login", data);
    }
  }
}

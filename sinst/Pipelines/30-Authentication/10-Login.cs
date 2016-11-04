using System.Collections.Specialized;
using System.Linq;
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

      var loginPrefix = GetLoginPrefix(submit);

      var data = new NameValueCollection
      {
        { "__EVENTTARGET", "" },
        { "__EVENTARGUMENT", ""},
        { "__VIEWSTATE", viewState },
        { "__VIEWSTATEGENERATOR", viewStateGenerator },
        { "__EVENTVALIDATION", eventValidation },
        { loginPrefix + "UserName", pipelineContext.Model.Connection.Credentials.UserName },
        { loginPrefix + "Password", pipelineContext.Model.Connection.Credentials.Password },
        { submit, submitValue }
      };

      client.Post("sitecore/login", data);
    }

    /// <summary>
    /// Gets the login prefix.
    /// </summary>
    /// <param name="submit">The submit.</param>
    /// <returns></returns>
    private static string GetLoginPrefix(string submit)
    {
      var loginPrefix = submit.Split('$').Reverse().Skip(1).FirstOrDefault() ?? string.Empty;

      if (!string.IsNullOrEmpty(loginPrefix))
      {
        loginPrefix += "$";
      }
      return loginPrefix;
    }
  }
}

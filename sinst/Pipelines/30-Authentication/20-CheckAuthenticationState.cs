using System.Linq;
using Sitecore.Remote.Installation.Attributes;
using Sitecore.Remote.Installation.Installer;
using Sitecore.Remote.Installation.Models;
using Sitecore.Remote.Installation.Pipelines.Metadata;

namespace Sitecore.Remote.Installation.Pipelines
{
  /// <summary>
  ///   Check authentication state
  /// </summary>
  /// <seealso cref="Sitecore.Remote.Installation.Models.IHandler" />
  [Pipeline(PipelineNames.Authentication)]
  [Processor(20)]
  public class CheckAuthenticationState
  {
    /// <summary>
    ///   Initializes a new installer of the <see cref="CheckAuthenticationState" /> class.
    /// </summary>
    public CheckAuthenticationState() : this(UiInstaller.Instance)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="CheckAuthenticationState" /> class.
    /// </summary>
    /// <param name="installer">The installer.</param>
    public CheckAuthenticationState(UiInstaller installer)
    {
      this.Installer = installer;
    }

    /// <summary>
    ///   Gets the installer.
    /// </summary>
    /// <value>
    ///   The installer.
    /// </value>
    public UiInstaller Installer { get; }

    /// <summary>
    ///   Processes the specified client.
    /// </summary>
    /// <param name="context">The context.</param>
    public void Process(IPipelineContext<IHttpClient> context)
    {
      var client = context.Model;

      if (!HasAuthCookie(client))
      {
        this.Installer.Events.RaiseOutputRequired("The authentication failed, please check the specified credentials.");
        this.Installer.Events.RaiseFailure();

        context.Aborted = true;
      }
    }

    /// <summary>
    ///   Determines whether [has authentication cookie] [the specified client].
    /// </summary>
    /// <param name="client">The client.</param>
    /// <returns>
    ///   <c>true</c> if [has authentication cookie] [the specified client]; otherwise, <c>false</c>.
    /// </returns>
    private static bool HasAuthCookie(IHttpClient client)
    {
      return HasSitecore71AuthCookie(client) || HasSitecore81AuthCookie(client);
    }

    /// <summary>
    ///   Determines whether the specified client has sitecore81 authentication cookie.
    /// </summary>
    /// <param name="client">The client.</param>
    /// <returns>
    ///   <c>true</c> if [has sitecore81 authentication cookie] [the specified client]; otherwise, <c>false</c>.
    /// </returns>
    private static bool HasSitecore81AuthCookie(IHttpClient client)
    {
      return HasCookieValue(client, "Cookie", ".ASPXAUTH");
    }

    /// <summary>
    ///   Determines whether the specified client has sitecore71 authentication cookie.
    /// </summary>
    /// <param name="client">The client.</param>
    /// <returns>
    ///   <c>true</c> if [has sitecore71 authentication cookie] [the specified client]; otherwise, <c>false</c>.
    /// </returns>
    private static bool HasSitecore71AuthCookie(IHttpClient client)
    {
      return HasCookieValue(client, "Set-Cookie", ".ASPXAUTH");
    }

    /// <summary>
    ///   Determines whether [has cookie value] [the specified client].
    /// </summary>
    /// <param name="client">The client.</param>
    /// <param name="cookieName">Name of the cookie.</param>
    /// <param name="value">The value.</param>
    /// <returns>
    ///   <c>true</c> if the specified client has cookie value ; otherwise, <c>false</c>.
    /// </returns>
    private static bool HasCookieValue(IHttpClient client, string cookieName, string value)
    {
      return client.Headers.AllKeys.Contains(cookieName) && client.Headers[cookieName].Contains(value);
    }
  }
}
using System;
using System.Collections.Specialized;
using System.Net;
using Sitecore.Remote.Installation.Diagnostic;
using Sitecore.Remote.Installation.Extensions;
using Sitecore.Remote.Installation.Models;

namespace Sitecore.Remote.Installation.Client
{
  /// <summary>
  /// Http package client
  /// </summary>
  /// <seealso cref="Sitecore.Remote.Installation.Models.IHttpClient" />
  public class HttpClient : IHttpClient
  {
    /// <summary>
    /// Initializes the <see cref="HttpClient"/> class.
    /// </summary>
    static HttpClient()
    {
      ServicePointManager.ServerCertificateValidationCallback = (sender, certification, chain, sslPolicyErrors) => true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpClient" /> class.
    /// </summary>
    /// <param name="conneciton">The conneciton.</param>
    public HttpClient(IConnection conneciton)
    {
      Assert.ArgumentNotNull(conneciton, nameof(conneciton));
      Assert.ArgumentNotNullOrEmpty(conneciton.Host, nameof(conneciton.Host));

      this.Headers = new WebHeaderCollection();
      this.Connection = conneciton;
      this.BaseUrl = new Uri(conneciton.Host).GetBaseUrl();
      this.Cookie = new CookieContainer();
    }

    /// <summary>
    ///   Gets the connection.
    /// </summary>
    /// <value>
    ///   The connection.
    /// </value>
    public virtual IConnection Connection { get; }

    /// <summary>
    ///   Gets the base URL.
    /// </summary>
    /// <value>
    ///   The base URL.
    /// </value>
    public virtual string BaseUrl { get; }

    /// <summary>
    ///   Gets the headers.
    /// </summary>
    /// <value>
    ///   The headers.
    /// </value>
    public virtual NameValueCollection Headers { get; }

    /// <summary>
    /// Gets the cookie.
    /// </summary>
    /// <value>
    /// The cookie.
    /// </value>
    public virtual CookieContainer Cookie { get; }

    /// <summary>
    /// Creates the request.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public virtual HttpWebRequest CreateRequest(string url)
    {
      var request = WebRequest.CreateHttp(this.BaseUrl + "/" + url);

      this.Headers.CopyTo(request.Headers, "Cookie");

      request.Accept = "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
      request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.59 Safari/537.36";
      request.CookieContainer = this.Cookie;

      request.ContentType = "application/x-www-form-urlencoded";

      return request;
    }

    /// <summary>
    /// Posts the specified URL.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="formData">The form data.</param>
    /// <param name="headers">The headers.</param>
    /// <returns></returns>
    public virtual string Post(string url, NameValueCollection formData, NameValueCollection headers = null)
    {
      Assert.ArgumentNotNull(url, nameof(url));
      Assert.ArgumentNotNull(formData, nameof(formData));

      return this.Request(url, "POST", formData.Stringify());
    }

    /// <summary>
    ///   Gets the specified URL.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <returns></returns>
    public virtual string Get(string url)
    {
      Assert.ArgumentNotNull(url, nameof(url));

      return this.Request(url, "GET");
    }

    /// <summary>
    ///   Requests this instance.
    /// </summary>
    public virtual string Request(string url, string method, string data = null)
    {
      var request = this.CreateRequest(url);
      request.Method = method;
      request.ContentLength = data?.Length ?? 0;
      request.Write(data);

      var response = request.GetResponse() as HttpWebResponse;

      if (response != null)
      {
        response.Headers.CopyTo(this.Headers, "Cookie");
        this.Cookie.Add(response.Cookies);

        return response.Read();
      }

      return null;
    }
  }
}
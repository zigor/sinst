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
    ///   Initializes a new instance of the <see cref="HttpClient" /> class.
    /// </summary>
    /// <param name="conneciton">The conneciton.</param>
    public HttpClient(IConnection conneciton)
    {
      Assert.ArgumentNotNull(conneciton, nameof(conneciton));
      Assert.ArgumentNotNullOrEmpty(conneciton.Host, nameof(conneciton.Host));

      this.Connection = conneciton;
      this.Headers = new WebHeaderCollection();
      this.BaseUrl = new Uri(conneciton.Host).GetBaseUrl();
    }

    /// <summary>
    ///   Gets the connection.
    /// </summary>
    /// <value>
    ///   The connection.
    /// </value>
    public IConnection Connection { get; }

    /// <summary>
    ///   Gets the base URL.
    /// </summary>
    /// <value>
    ///   The base URL.
    /// </value>
    public string BaseUrl { get; }

    /// <summary>
    ///   Gets the headers.
    /// </summary>
    /// <value>
    ///   The headers.
    /// </value>
    public NameValueCollection Headers { get; }
    
    /// <summary>
    ///   Posts the specified URL.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="formData">The form data.</param>
    /// <returns></returns>
    public string Post(string url, NameValueCollection formData)
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
    public string Get(string url)
    {
      Assert.ArgumentNotNull(url, nameof(url));

      return this.Request(url, "GET");
    }

    /// <summary>
    ///   Requests this instance.
    /// </summary>
    public string Request(string url, string method, string data = null)
    {
      var request = WebRequest.CreateHttp(this.BaseUrl + "/" + url);

      this.Headers.CopyTo(request.Headers, "Cookie");
      
      request.Method = "POST";

      request.Accept = "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
      request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.59 Safari/537.36";
      request.CookieContainer = new CookieContainer();
      
      
      request.ContentType = "application/x-www-form-urlencoded";
      request.ContentLength = data?.Length ?? 0;
      request.Write(data);

      var response = request.GetResponse() as HttpWebResponse;

      if (response != null)
      {
        response.Headers.CopyTo(this.Headers, "Cookie");

        return response.Read();
      }

      return null;
    }
  }
}
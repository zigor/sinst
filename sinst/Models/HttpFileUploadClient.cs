using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using Sitecore.Remote.Installation.Client;
using Sitecore.Remote.Installation.Extensions;

namespace Sitecore.Remote.Installation.Models
{
  /// <summary>
  ///   Http file upload client
  /// </summary>
  /// <seealso cref="Sitecore.Remote.Installation.Models.IHttpClient" />
  public class HttpFileUploadClient
  {
    /// <summary>
    /// The client
    /// </summary>
    private readonly IHttpClient client;

    /// <summary>
    ///   Initializes a new instance of the <see cref="HttpFileUploadClient" /> class.
    /// </summary>
    /// <param name="client">The client.</param>
    public HttpFileUploadClient(IHttpClient client)
    {
      this.client = client;
    }

    /// <summary>
    /// Posts the specified URL.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="fileHandler">The file handler.</param>
    /// <param name="file">The file.</param>
    /// <param name="fileContentType">Type of the file content.</param>
    /// <param name="formData">The form data.</param>
    /// <returns></returns>
    public string Post(string url, string fileHandler, string file, string fileContentType, NameValueCollection formData)
    {
      var request = WebRequest.CreateHttp(this.client.Connection.Host + "/" + url);

      request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.59 Safari/537.36";
      request.CookieContainer = this.client.Cookie;

      request.Method = "POST";
      request.KeepAlive = true;
      
      this.client.Headers.CopyTo(request.Headers, "Cookie");
      
      request.Write(fileHandler, file, fileContentType, formData);

      var response = request.GetResponse() as HttpWebResponse;
      return response.Read();      
    }
  }
}
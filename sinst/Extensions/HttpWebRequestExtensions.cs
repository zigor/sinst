using System.IO;
using System.Net;
using System.Text;
using Sitecore.Remote.Installation.Diagnostic;

namespace Sitecore.Remote.Installation.Extensions
{
  /// <summary>
  /// Extensions for HttpWebResponse
  /// </summary>
  public static class HttpWebRequestExtensions
  {
    /// <summary>
    /// Writes the specified data.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="data">The data.</param>
    public static void Write(this HttpWebRequest request, string data)
    {
      Assert.ArgumentNotNull(request, nameof(request));

      if (string.IsNullOrEmpty(data))
      {
        return;
      }

      using (var streamWriter = new StreamWriter(request.GetRequestStream()))
      {
        streamWriter.Write(data);
        streamWriter.Close();
      }
    }
  }
}

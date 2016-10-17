using System.IO;
using System.Net;

namespace Sitecore.Remote.Installation.Extensions
{
  /// <summary>
  /// Extensions for HttpWebResponse
  /// </summary>
  public static class HttpWebResponseExtensions
  {
    /// <summary>
    /// Reads the specified response.
    /// </summary>
    /// <param name="response">The response.</param>
    /// <returns></returns>
    public static string Read(this HttpWebResponse response)
    {
      if (response.StatusCode == HttpStatusCode.OK)
      {
        using (var responseStream = response.GetResponseStream())
        {
          if (responseStream != null)
          {
            using (StreamReader reader = new StreamReader(responseStream))
            {
              string content = reader.ReadToEnd();

              reader.Close();
              response.Close();

              return content;
            }
          }
        }
      }

      return null;
    }
  }
}

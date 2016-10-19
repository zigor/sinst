using System;
using System.Collections.Specialized;
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

    /// <summary>
    /// Writes the specified file.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="fileHandler">The file handler.</param>
    /// <param name="file">The file.</param>
    /// <param name="fileContentType">Type of the file content.</param>
    /// <param name="formData">The form data.</param>
    /// <exception cref="ArgumentException">File " + file + " does not exist</exception>
    public static void Write(this HttpWebRequest request, string fileHandler, string file, string fileContentType, NameValueCollection formData)
    {
      Assert.ArgumentNotNull(request, nameof(request));
      Assert.ArgumentNotNullOrEmpty(fileHandler, nameof(fileHandler));
      Assert.ArgumentNotNullOrEmpty(file, nameof(file));
      Assert.ArgumentNotNullOrEmpty(fileContentType, nameof(fileContentType));
      Assert.ArgumentNotNull(formData, nameof(formData));

      if (!File.Exists(file))
      {
        throw new ArgumentException("File " + file + " does not exist", nameof(file));
      }

      var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
      var boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

      request.ContentType = "multipart/form-data; boundary=" + boundary;

      using (var requestStream = request.GetRequestStream())
      {
        var formDataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
        foreach (string key in formData.Keys)
        {
          requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);

          var formitem = string.Format(formDataTemplate, key, formData[key]);
          var formItemBytes = Encoding.UTF8.GetBytes(formitem);
          requestStream.Write(formItemBytes, 0, formItemBytes.Length);
        }

        requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
        
        var headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
        var header = string.Format(headerTemplate, fileHandler, Path.GetFileName(file), fileContentType);
        var headerbytes = Encoding.UTF8.GetBytes(header);
        requestStream.Write(headerbytes, 0, headerbytes.Length);

        using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
        {
          var buffer = new byte[4096];
          var bytesRead = 0;
          while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
          {
            requestStream.Write(buffer, 0, bytesRead);
          }
          fileStream.Close();
        }

        requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
        requestStream.Close();
      }
    }
  }
}

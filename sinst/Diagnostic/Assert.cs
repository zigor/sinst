using System;

namespace Sitecore.Remote.Installation.Diagnostic
{
  /// <summary>
  /// Asserts
  /// </summary>
  public class Assert
  {
    /// <summary>
    ///   Arguments the not null or empty.
    /// </summary>
    /// <param name="argument">The argument.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <exception cref="System.ArgumentNullException">
    ///   Null ids are not allowed.
    ///   or
    /// </exception>
    /// <exception cref="System.ArgumentException">
    ///   Empty strings are not allowed.
    ///   or
    ///   Empty strings are not allowed.
    /// </exception>
    public static void ArgumentNotNullOrEmpty(string argument, string argumentName)
    {
      if (!string.IsNullOrEmpty(argument))
      {
        return;
      }

      if (argument == null)
      {
        if (argumentName != null)
        {
          throw new ArgumentNullException(argumentName, "Null strings are not allowed.");
        }

        throw new ArgumentNullException();
      }

      if (argumentName != null)
      {
        throw new ArgumentException("Empty strings are not allowed.", argumentName);
      }

      throw new ArgumentException("Empty strings are not allowed.");
    }

    /// <summary>
    /// Arguments the not null.
    /// </summary>
    /// <param name="argument">The argument.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <exception cref="System.ArgumentNullException">
    /// </exception>
    public static void ArgumentNotNull(object argument, string argumentName)
    {
      if (argument != null)
      {
        return;
      }

      if (argumentName != null)
      {
        throw new ArgumentNullException(argumentName);

      }
      throw new ArgumentNullException();
    }
  }
}
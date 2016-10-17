namespace Sitecore.Remote.Installation.Models
{
  /// <summary>
  /// Context definition
  /// </summary>
  /// <typeparam name="TModel"></typeparam>
  public interface IPipelineContext<TModel>
  {
    /// <summary>
    /// Gets the model.
    /// </summary>
    /// <value>
    /// The model.
    /// </value>
    TModel Model { get; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="IPipelineContext{T}" /> is aborted.
    /// </summary>
    /// <value>
    ///   <c>true</c> if aborted; otherwise, <c>false</c>.
    /// </value>
    bool Aborted { get; set; }

    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    /// <value> 
    /// The state.
    /// </value>
    string State { get; set; }
  }
}
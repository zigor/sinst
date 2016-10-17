using Sitecore.Remote.Installation.Models;

namespace Sitecore.Remote.Installation.Pipelines.Core
{
  /// <summary>
  /// Pipeline context
  /// </summary>
  /// <typeparam name="TModel"></typeparam>
  /// <seealso cref="Sitecore.Remote.Installation.Models.IPipelineContext{T}" />
  public class PipelineContext<TModel> : IPipelineContext<TModel>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="PipelineContext{T}"/> class.
    /// </summary>
    /// <param name="model">The model.</param>
    public PipelineContext(TModel model)
    {
      this.Model = model;
    }

    /// <summary>
    ///   Gets the model.
    /// </summary>
    /// <value>
    ///   The model.
    /// </value>
    public TModel Model { get; }

    /// <summary>
    ///   Gets or sets a value indicating whether this <see cref="T:Sitecore.Remote.Installation.Models.IPipelineContext`1" />
    ///   is aborted.
    /// </summary>
    /// <value>
    ///   <c>true</c> if aborted; otherwise, <c>false</c>.
    /// </value>
    public bool Aborted { get; set; }

    /// <summary>
    ///   Gets or sets the state.
    /// </summary>
    /// <value>
    ///   The state.
    /// </value>
    public string State { get; set; }
  }
}
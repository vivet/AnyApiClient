using Newtonsoft.Json;

namespace AnyApiClient.Requests.Interfaces
{
    /// <summary>
    /// Base interface for requests.
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// Route.
        /// </summary>
        [JsonIgnore]
        string Route { get; }
    }
}
namespace AnyApiClient.Requests.Interfaces
{
    /// <summary>
    /// Base interface for post requests (POST).
    /// </summary>
    public interface IRequestPost : IRequest
    {
        /// <summary>
        /// Get Body.
        /// </summary>
        /// <returns>An object, containing the body.</returns>
        object GetBody();
    }
}
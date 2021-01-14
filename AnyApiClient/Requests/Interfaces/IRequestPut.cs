namespace AnyApiClient.Requests.Interfaces
{
    /// <summary>
    /// Base interface for put requests (PUT).
    /// </summary>
    public interface IRequestPut : IRequest
    {
        /// <summary>
        /// Get Body.
        /// </summary>
        /// <returns>An object, containing the body.</returns>
        object GetBody();
    }
}
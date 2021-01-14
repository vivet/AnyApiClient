using System;

namespace AnyApiClient.Requests.Attributes
{
    /// <summary>
    /// Query Attribute.
    /// </summary>
    public class QueryAttribute : Attribute
    {
        /// <summary>
        /// Name.
        /// </summary>
        public virtual string Name { get; set; }
    }
}
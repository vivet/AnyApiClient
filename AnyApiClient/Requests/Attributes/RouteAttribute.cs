using System;

namespace AnyApiClient.Requests.Attributes
{
    /// <summary>
    /// Route Attribute.
    /// </summary>
    public class RouteAttribute : Attribute
    {
        /// <summary>
        /// Order.
        /// </summary>
        public virtual int Order { get; set; }
    }
}
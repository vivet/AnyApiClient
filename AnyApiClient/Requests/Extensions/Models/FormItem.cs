using System;

namespace AnyApiClient.Requests.Extensions.Models
{
    /// <summary>
    /// Form Item.
    /// </summary>
    internal class FormItem
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual object Value { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual Type Type { get; set; }
    }
}
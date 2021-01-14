using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AnyApiClient.Requests.Attributes;
using AnyApiClient.Requests.Extensions.Models;
using AnyApiClient.Requests.Interfaces;

namespace AnyApiClient.Requests.Extensions
{
    /// <summary>
    /// Request Extensions.
    /// </summary>
    internal static class RequestExtensions
    {
        /// <summary>
        /// Get Route.
        /// Get the route parameters of the passed <paramref name="request"/>, defined by properties having <see cref="RouteAttribute"/>.
        /// </summary>
        /// <typeparam name="TRequest">The type of request.</typeparam>
        /// <param name="request">The request of type <typeparamref name="TRequest"/>.</param>
        /// <returns>The route as string.</returns>
        internal static string GetRoute<TRequest>(this TRequest request)
            where TRequest :IRequest
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var parameters = typeof(TRequest)
                .GetProperties()
                .Select(x =>
                {
                    var property = x;
                    var attribute = x.GetCustomAttribute<RouteAttribute>();

                    return (property, attribute);
                })
                .Where(x => x.attribute != null)
                .OrderBy(x => x.attribute.Order)
                .Select(x =>
                {
                    var value = x.property.GetValue(request);

                    return value ?? string.Empty;
                });

            return string.Format(request.Route, parameters);
        }

        /// <summary>
        /// Get Querystring.
        /// Get the querystring parameters of the passed <paramref name="request"/>, defined by properties having <see cref="QueryAttribute"/>.
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static string GetQuerystring<TRequest>(this TRequest request)
            where TRequest : IRequest
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var parameters = typeof(TRequest)
                .GetProperties()
                .Select(x =>
                {
                    var property = x;
                    var attribute = x.GetCustomAttribute<QueryAttribute>();

                    return (property, attribute);
                })
                .Where(x => x.attribute != null)
                .Select(x =>
                {
                    var key = x.attribute.Name ?? x.property.Name;
                    var value = x.property.GetValue(request);

                    if (value == null)
                        return Uri.EscapeDataString(key);

                    return Uri.EscapeDataString(key) + "=" + Uri.EscapeDataString(value.ToString());
                });

            return string.Join("&", parameters);
        }

        /// <summary>
        /// Get Form.
        /// Get the form parameters of the passed <paramref name="request"/>, defined by properties having <see cref="FormAttribute"/>.
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static IEnumerable<FormItem> GetForm<TRequest>(this TRequest request)
            where TRequest : IRequest
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return typeof(TRequest)
                .GetProperties()
                .Where(x => x.GetCustomAttribute<FormAttribute>() != null)
                .Select(x =>
                {
                    var value = x.GetValue(request);
                    
                    return new FormItem
                    {
                        Type = x.PropertyType,
                        Name = x.Name,
                        Value = value
                    };
                });
        }
    }
}
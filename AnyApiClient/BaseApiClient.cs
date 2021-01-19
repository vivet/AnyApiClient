using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AnyApiClient.Const;
using AnyApiClient.Models;
using AnyApiClient.Requests.Extensions;
using AnyApiClient.Requests.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace AnyApiClient
{
    /// <summary>
    /// Base Api (abstract).
    /// </summary>
    public abstract class BaseApiClient
    {
        private readonly ApiOptions apiOptions;
        private readonly HttpClient httpClient;
        private readonly HttpClientHandler httpClientHandler = new HttpClientHandler
        {
            AllowAutoRedirect = true,
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };
        private readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            ContractResolver = new DefaultContractResolver()
        };

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="apiOptions">The <see cref="ApiOptions"/>.</param>
        protected BaseApiClient(ApiOptions apiOptions)
        {
            this.apiOptions = apiOptions ?? throw new ArgumentNullException(nameof(apiOptions));

            this.httpClient = new HttpClient(this.httpClientHandler)
            {
                Timeout = new TimeSpan(0, 0, this.apiOptions.TimeoutInSeconds)
            };

            this.httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue(HttpContentType.JSON));

            this.jsonSerializerSettings.Converters
                .Add(new StringEnumConverter());
        }

        /// <summary>
        /// Invokes a http GET request.
        /// </summary>
        /// <typeparam name="TRequest">The request type, of type <see cref="IRequestGet"/>.</typeparam>
        /// <typeparam name="TResponse">The response type.</typeparam>
        /// <param name="request">The <see cref="IRequestGet"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>The response.</returns>
        protected virtual async Task<TResponse> Get<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : class, IRequestGet
            where TResponse : class
        {
            if (request == null) 
                throw new ArgumentNullException(nameof(request));

            using var httpRequest = this.GetHttpRequest(request);

            using var httpResponse = await this.httpClient
                .SendAsync(httpRequest, cancellationToken);

            return await this.GetReponse<TResponse>(httpResponse, cancellationToken);
        }

        /// <summary>
        /// Invokes a http PUT request.
        /// </summary>
        /// <typeparam name="TRequest">The request type, of type <see cref="IRequestGet"/>.</typeparam>
        /// <param name="request">The <see cref="IRequestPut"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>Void.</returns>
        protected virtual async Task Put<TRequest>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : class, IRequestPost
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using var httpRequest = this.GetHttpRequest(request);

            var body = request.GetBody();
            var content = body == null ? string.Empty : JsonConvert.SerializeObject(body, this.jsonSerializerSettings);

            httpRequest.Content = new StringContent(content, Encoding.UTF8, "application/json");

            using var httpResponse = await this.httpClient
                .SendAsync(httpRequest, cancellationToken);
        }

        /// <summary>
        /// Invokes a http PUT request.
        /// </summary>
        /// <typeparam name="TRequest">The request type, of type <see cref="IRequestGet"/>.</typeparam>
        /// <typeparam name="TResponse">The response type.</typeparam>
        /// <param name="request">The <see cref="IRequestPut"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>The response.</returns>
        protected virtual async Task<TResponse> Put<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : class, IRequestPost
            where TResponse : class
        {
            if (request == null) 
                throw new ArgumentNullException(nameof(request));

            using var httpRequest = this.GetHttpRequest(request);

            var body = request.GetBody();
            var content = body == null ? string.Empty : JsonConvert.SerializeObject(body, this.jsonSerializerSettings);

            httpRequest.Content = new StringContent(content, Encoding.UTF8, "application/json");

            using var httpResponse = await this.httpClient
                .SendAsync(httpRequest, cancellationToken);

            return await this.GetReponse<TResponse>(httpResponse, cancellationToken);
        }

        /// <summary>
        /// Invokes a http POST request.
        /// </summary>
        /// <typeparam name="TRequest">The request type, of type <see cref="IRequestGet"/>.</typeparam>
        /// <param name="request">The <see cref="IRequestPost"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>Void.</returns>
        protected virtual async Task Post<TRequest>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : class, IRequestPost
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using var httpRequest = this.GetHttpRequest(request);

            var body = request.GetBody();
            var content = body == null ? string.Empty : JsonConvert.SerializeObject(body, this.jsonSerializerSettings);

            httpRequest.Content = new StringContent(content, Encoding.UTF8, "application/json");

            using var httpResponse = await this.httpClient
                .SendAsync(httpRequest, cancellationToken);
        }

        /// <summary>
        /// Invokes a http POST request.
        /// </summary>
        /// <typeparam name="TRequest">The request type, of type <see cref="IRequestGet"/>.</typeparam>
        /// <typeparam name="TResponse">The response type.</typeparam>
        /// <param name="request">The <see cref="IRequestPost"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>The response.</returns>
        protected virtual async Task<TResponse> Post<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : class, IRequestPost
            where TResponse : class
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using var httpRequest = this.GetHttpRequest(request);

            var body = request.GetBody();
            var content = body == null ? string.Empty : JsonConvert.SerializeObject(body, this.jsonSerializerSettings);

            httpRequest.Content = new StringContent(content, Encoding.UTF8, "application/json");

            using var httpResponse = await this.httpClient
                .SendAsync(httpRequest, cancellationToken);

            return await this.GetReponse<TResponse>(httpResponse, cancellationToken);
        }

        /// <summary>
        /// Invokes a http POST (form) request.
        /// </summary>
        /// <typeparam name="TRequest">The request type, of type <see cref="IRequestGet"/>.</typeparam>
        /// <param name="request">The <see cref="IRequestPostForm"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>Void.</returns>
        protected virtual async Task PostForm<TRequest>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : class, IRequestPostForm
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using var httpRequest = this.GetHttpRequest(request);
            using var formContent = new MultipartFormDataContent();

            foreach (var x in request.GetForm())
            {
                if (x.Type == typeof(FileInfo))
                {
                    var value = (FileInfo)x.Value;
                    var filename = value.FullName;

                    if (!File.Exists(filename))
                        throw new FileNotFoundException($"File: '{filename}' not found.");

                    var bytes = File.ReadAllBytes(filename);
                    var fileContent = new ByteArrayContent(bytes);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(HttpContentType.FORM);
                    
                    formContent
                        .Add(fileContent, "file", Path.GetFileName(filename));
                }
                else
                {
                    var value = x.Value.ToString();

                    formContent
                        .Add(new StringContent(value), x.Name);
                }
            }

            httpRequest.Content = formContent;

            using var httpResponse = await this.httpClient
                .SendAsync(httpRequest, cancellationToken);
        }

        /// <summary>
        /// Invokes a http POST (form) request.
        /// </summary>
        /// <typeparam name="TRequest">The request type, of type <see cref="IRequestGet"/>.</typeparam>
        /// <typeparam name="TResponse">The response type.</typeparam>
        /// <param name="request">The <see cref="IRequestPostForm"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>The response.</returns>
        protected virtual async Task<TResponse> PostForm<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : class, IRequestPostForm
            where TResponse : class
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using var httpRequest = this.GetHttpRequest(request);
            using var formContent = new MultipartFormDataContent();

            foreach (var x in request.GetForm())
            {
                if (x.Type == typeof(FileInfo))
                {
                    var value = (FileInfo)x.Value;
                    var filename = value.FullName;

                    if (!File.Exists(filename))
                        throw new FileNotFoundException($"File: '{filename}' not found.");

                    var bytes = File.ReadAllBytes(filename);
                    var fileContent = new ByteArrayContent(bytes);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(HttpContentType.FORM);

                    formContent
                        .Add(fileContent, "file", Path.GetFileName(filename));
                }
                else
                {
                    var value = x.Value.ToString();

                    formContent
                        .Add(new StringContent(value), x.Name);
                }
            }

            httpRequest.Content = formContent;

            using var httpResponse = await this.httpClient
                .SendAsync(httpRequest, cancellationToken);

            return await this.GetReponse<TResponse>(httpResponse, cancellationToken);
        }

        /// <summary>
        /// Invokes a http DELETE request.
        /// </summary>
        /// <typeparam name="TRequest">The request type, of type <see cref="IRequestGet"/>.</typeparam>
        /// <param name="request">The <see cref="IRequestGet"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>Void.</returns>
        protected virtual async Task Delete<TRequest>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : class, IRequestDelete
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using var httpRequest = this.GetHttpRequest(request);

            using var httpResponse = await this.httpClient
                .SendAsync(httpRequest, cancellationToken);
        }

        /// <summary>
        /// Invokes a http DELETE request.
        /// </summary>
        /// <typeparam name="TRequest">The request type, of type <see cref="IRequestGet"/>.</typeparam>
        /// <typeparam name="TResponse">The response type.</typeparam>
        /// <param name="request">The <see cref="IRequestGet"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>The response.</returns>
        protected virtual async Task<TResponse> Delete<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : class, IRequestDelete
            where TResponse : class
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using var httpRequest = this.GetHttpRequest(request);

            using var httpResponse = await this.httpClient
                .SendAsync(httpRequest, cancellationToken);

            return await this.GetReponse<TResponse>(httpResponse, cancellationToken);
        }

        private Uri GetUri<TRequest>(TRequest request)
            where TRequest : IRequest
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var protocol = this.apiOptions.UseSsl 
                ? "https://" 
                : "http://";
            var host = this.apiOptions.Host.EndsWith("/") 
                ? this.apiOptions.Host.Substring(0, this.apiOptions.Host.Length - 1) 
                : this.apiOptions.Host;
            var port = this.apiOptions.Port;
            var root = this.apiOptions.Root.EndsWith("/") 
                ? this.apiOptions.Root.Substring(0, this.apiOptions.Root.Length - 1) 
                : this.apiOptions.Root;
            var route = request.GetRoute();
            var queryString = request.GetQuerystring();

            return new Uri($"{protocol}{host}:{port}/{root}/{route}?{queryString}");
        }
        private HttpMethod GetMethod<TRequest>(TRequest request)
            where TRequest : IRequest
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return request switch
            {
                IRequestGet _ => HttpMethod.Get,
                IRequestPut _ => HttpMethod.Put,
                IRequestPost _ => HttpMethod.Post,
                IRequestPostForm _ => HttpMethod.Post,
                IRequestDelete _ => HttpMethod.Delete,
                IRequestOptions _ => HttpMethod.Options,
                _ => throw new NotSupportedException()
            };
        }
        private HttpRequestMessage GetHttpRequest<TRequest>(TRequest request)
            where TRequest : IRequest
        {
            if (request == null) 
                throw new ArgumentNullException(nameof(request));
            
            var uri = this.GetUri(request);
            var method = this.GetMethod(request);

            var jwtToken = ""; // TODO: jwt-token.

            var httpRequst = new HttpRequestMessage(method, uri);
            httpRequst.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            httpRequst.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentCulture.Name));

            return httpRequst;
        }
        private async Task<TResponse> GetReponse<TResponse>(HttpResponseMessage httpResponse, CancellationToken cancellationToken = default)
            where TResponse : class
        {
            if (httpResponse == null)
                throw new ArgumentNullException(nameof(httpResponse));

            switch (httpResponse.StatusCode)
            {
                // TODO: Handle all cases?
                case HttpStatusCode.NotFound:
                    return default;

                case HttpStatusCode.Unauthorized:
                    throw new AggregateException(new UnauthorizedAccessException());

                case HttpStatusCode.BadRequest:
                case HttpStatusCode.InternalServerError:
                    var errorContent = await httpResponse.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<Error>(errorContent);

                    if (this.apiOptions.UseExposeErrors)
                    {
                        throw new AggregateException(error.Exceptions.Select(x => new InvalidOperationException(x)));
                    }

                    break;
            }

            httpResponse
                .EnsureSuccessStatusCode();

            var contentType = httpResponse.Content.Headers.ContentType.MediaType;

            // TODO: Add all possible media types.
            switch (contentType)
            {
                case HttpContentType.HTML:
                case HttpContentType.PDF:
                case HttpContentType.ZIP:
                    return await httpResponse.Content.ReadAsStreamAsync() as TResponse;

                case HttpContentType.JSON:
                    var successContent = await httpResponse.Content
                        .ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<TResponse>(successContent);

                default:
                    throw new NotSupportedException(contentType);
            }
        }
    }
}
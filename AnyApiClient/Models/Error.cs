using System;
using System.Linq;
using System.Net;

namespace AnyApiClient.Models
{
    /// <summary>
    /// Error.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Message.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        public string[] Exceptions { get; set; } = new string[0];

        /// <summary>
        /// Status Code.
        /// </summary>
        public int StatusCode { get; set; } = 500;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Error()
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/>.</param>
        public Error(Exception exception)
            : this()
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            var baseException = exception.GetBaseException();

            this.Summary = "Internal Server Error";
            this.StatusCode = (int)HttpStatusCode.InternalServerError;
            this.Exceptions = new[]
            {
                $"{baseException.GetType().Name} - {baseException.Message}"
            };
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var exceptionsString = this.Exceptions
                .Aggregate($"Messages:{Environment.NewLine}", (current, exception) => current + exception + Environment.NewLine);

            return $"{this.StatusCode} {this.Summary}{Environment.NewLine}Messages:{Environment.NewLine}{exceptionsString}";
        }
    }
}
namespace PhungDKH.Catalog.Api.Infrastructure.Exceptions
{
    using System;

    /// <summary>
    ///    Exception type for app exceptions.
    /// </summary>
    /// <seealso cref="System.Exception"/>
    public class PhungDKHMicroserviceDomainException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhungDKHMicroserviceDomainException"/> class.
        /// </summary>
        public PhungDKHMicroserviceDomainException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhungDKHMicroserviceDomainException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public PhungDKHMicroserviceDomainException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhungDKHMicroserviceDomainException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public PhungDKHMicroserviceDomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

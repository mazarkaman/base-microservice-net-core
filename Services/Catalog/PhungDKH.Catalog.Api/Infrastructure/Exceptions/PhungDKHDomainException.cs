namespace PhungDKH.Catalog.Api.Infrastructure.Exceptions
{
    using System;

    /// <summary>
    ///    Exception type for app exceptions.
    /// </summary>
    /// <seealso cref="System.Exception"/>
    public class PhungDKHDomainException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhungDKHDomainException"/> class.
        /// </summary>
        public PhungDKHDomainException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhungDKHDomainException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public PhungDKHDomainException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhungDKHDomainException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public PhungDKHDomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

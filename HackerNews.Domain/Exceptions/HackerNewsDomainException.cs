using System;

namespace HackerNews.Domain.Exceptions
{
    public class HackerNewsDomainException : Exception
    {
        public HackerNewsDomainException()
        { }

        public HackerNewsDomainException(string message)
            : base(message)
        { }

        public HackerNewsDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }

    }
}



using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection.PortableExecutable;

namespace Joiniv.Domain.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public ApiException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}   
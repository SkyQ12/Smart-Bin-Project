using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartBin.Infrastructure.Domain.Exceptions
{
    public class ResourceNotfoundException : Exception
    {
        public ResourceNotfoundException()
        {
        }

        public ResourceNotfoundException(string? message) : base(message)
        {
        }

        public ResourceNotfoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

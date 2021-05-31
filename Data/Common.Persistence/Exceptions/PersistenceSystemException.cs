using System;
namespace Common.Persistence.Exceptions
{
    [Serializable]
    public class PersistenceSystemException : Exception
    {
        public PersistenceSystemException(string message) : base(message) { }
        public PersistenceSystemException(string message, Exception inner) : base(message, inner) { }
    }
}

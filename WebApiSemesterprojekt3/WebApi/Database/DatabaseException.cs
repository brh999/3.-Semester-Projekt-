using System.Runtime.Serialization;

namespace WebApi.Database
{
    [Serializable]
    internal class DatabaseException : Exception
    {
        public DatabaseException() : base()
        {

        }

        public DatabaseException(string message) : base(message)
        {

        }
    }
}
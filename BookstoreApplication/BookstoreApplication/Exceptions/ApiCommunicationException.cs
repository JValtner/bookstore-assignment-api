namespace BookstoreApplication.Exceptions
{
    public class ApiCommunicationException : Exception
    {
        public ApiCommunicationException(string message) : base(message)
        {
        }
    }

}

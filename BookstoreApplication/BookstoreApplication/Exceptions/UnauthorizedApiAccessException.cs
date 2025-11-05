namespace BookstoreApplication.Exceptions
{
    public class UnauthorizedApiAccessException : ApiCommunicationException
    {
        public UnauthorizedApiAccessException() : base("An invalid API key was provided for accessing external API.")
        {
        }
    }

}

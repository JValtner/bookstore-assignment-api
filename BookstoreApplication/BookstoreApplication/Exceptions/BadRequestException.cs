namespace BookstoreApplication.Exceptions
{
    public class BadRequestException: Exception
    {
        public BadRequestException(int id) : base($"Item with id {id} has problem.")
        {
        }
    }
}

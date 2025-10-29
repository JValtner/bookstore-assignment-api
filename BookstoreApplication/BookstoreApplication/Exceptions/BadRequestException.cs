namespace BookstoreApplication.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(int? id, string msg)
            : base(id.HasValue ? $"Item with id {id} has problem: {msg}" : msg)
        {
        }
    }
}

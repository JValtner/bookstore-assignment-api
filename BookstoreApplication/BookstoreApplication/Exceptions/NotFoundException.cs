namespace BookstoreApplication.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(int? id, string msg) 
            : base(id.HasValue? $"Item with id {id} could not be found: {msg}" : msg)
        {
        }
    }
}

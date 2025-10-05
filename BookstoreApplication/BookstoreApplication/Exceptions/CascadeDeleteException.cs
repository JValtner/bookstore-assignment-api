namespace BookstoreApplication.Exceptions
{
    public class CascadeDeleteException : Exception
    {
        public CascadeDeleteException(int id)
            : base($"Failed to cascade delete related entities with ID = {id}.")
        {
        }
    }
}


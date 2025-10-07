using BookstoreApplication.Models;

namespace BookstoreApplication.Utils
{
    public class SortTypeOption
    {
        public int Key { get; set; }
        public string Name { get; set; }

        public SortTypeOption(PublisherSortType sortType) // prethodno napravljena enumeracija
        {
            Key = (int)sortType; // celobrojni ključ koji će biti vezan za vrednost enumeracije (npr. 0 za NAME_ASCENDING, 1 za NAME_DESCENDING, itd.)
            Name = sortType.ToString(); // vrednost enumeracije (NAME_ASCENDING, NAME_DESCENDING, itd.)
        }
    }

}

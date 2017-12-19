using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreClasses
{
    public interface IBookstoreService
    {
        //Searches booklist for any book including the searchstring in either author or title
        Task<IEnumerable<IBook>> GetBooksAsync(string searchString);

        //Buy a book, removes one from stock and returns true if succesful
        bool BuyBook(IBook book);
    }
}

using BookStoreClasses;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BookStoreImplementation
{
    // Implementation of interface IBookstoreService, keeps a booklist from json "database" and handles searching in it
    public class BookstoreService : IBookstoreService
    {
        private static string BOOK_LIST_ADRESS = "https://raw.githubusercontent.com/contribe/contribe/dev/arbetsprov-net/books.json";
        private BookList books;

        public BookstoreService()
        {
            books = new BookList();
            FillBookListFromWeb(BOOK_LIST_ADRESS);
        }

        //Implementation of IBookStoreService:GetBooksAsync, returns search result as a task
        public Task<IEnumerable<IBook>> GetBooksAsync(string searchString)
        {
            return new Task<IEnumerable<IBook>>(() => GetBooks(searchString));
        }

        //Simple search function that checks if any book in the list contains any of the words in the searchstring in either title or author
        private IEnumerable<IBook> GetBooks(string searchString)
        {
            if (searchString == null)
                return null;
            BookList returnList = new BookList();
            char[] delimiters = { ' ', ',', '.', ':', '\t', '-', '+' };
            string[] searchWords = searchString.Split(delimiters);
            foreach(Book item in books)
            {
                foreach(string word in searchWords)
                {
                    if (item.Title.ToLower().Contains(word.ToLower()))
                    {
                        returnList.AddBook(item);
                        break;
                    }
                    else if (item.Author.ToLower().Contains(word.ToLower()))
                    {
                        returnList.AddBook(item);
                        break;
                    }
                }
            }
            return returnList;
        }

        //Gets books from web, returns false if url could not be found
        public bool FillBookListFromWeb(string bookListAdress)
        {
            WebClient client = new WebClient();
            try
            {
                string data = System.Text.Encoding.UTF8.GetString(client.DownloadData(bookListAdress));
                books = JsonConvert.DeserializeObject<BookList>(data);
                return true;
            }
            catch (System.Net.WebException e)
            {
                return false;
            }
        }

        //Removes one book from stock, returns false if book is already out of stock
        public bool BuyBook(IBook book)
        {
            foreach(Book b in books)
            {
                string a = b.Author;
                string t = b.Title;
                int s = b.InStock;
                decimal p = b.Price;
                if (b.Author == book.Author && b.Title == book.Title && b.Price == book.Price && b.InStock > 0)
                {
                    b.InStock -= 1;
                    return true;
                }
            }
            return false;
        }
    }
}

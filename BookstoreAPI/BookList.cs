using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace BookStoreImplementation
{
    //IEnumerable list of Book-objects
    [JsonObject]
    class BookList : IEnumerable<Book>
    {
        public List<Book> books;

        public BookList()
        {
            books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public void Clear()
        {
            books.Clear();
        }

        public IEnumerator<Book> GetEnumerator()
        {
            return this.books.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.books.GetEnumerator();
        }
    }
}

using BookStoreClasses;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace BookstoreAPI.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class OrderController : ApiController
    {
        //Short implementation for IBook for parsing from json requests
        public class SimpleBook : IBook
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public decimal Price { get; set; }
            public int InStock { get; set; }
        }
        public class SimpleBookList
        {
            public List<SimpleBook> books { get; set; }
        }

        // POST api/order
        public SimpleBookList Post(SimpleBookList bookList)
        {
            if (bookList == null)
                return null;
            for (int i = 0; i < bookList.books.Count; i++)
            {
                if (BookstoreConnection.Instance.Service.BuyBook(bookList.books[i]))
                {
                    bookList.books.RemoveAt(i);
                    i--;
                }
            }
            return bookList;
        }
    }
}
using BookStoreClasses;
using System.Collections.Generic;
using System.Web.Http;
using System.Collections;

namespace BookstoreAPI.Controllers
{
    public class OrderController : ApiController
    {
        public class SimpleBook : IBook
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public decimal Price { get; set; }
        }

        public class SimpleBookList
        {
            public List<SimpleBook> books { get; set; }
        }

        // POST api/order
        public SimpleBookList Post(SimpleBookList books)
        {
            foreach(SimpleBook b in books.books)
            {
                if(BookstoreConnection.Instance.Service.BuyBook(b))
                    books.books.Remove(b);
            }
            return books;
        }
    }
}
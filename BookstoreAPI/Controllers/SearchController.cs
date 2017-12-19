using BookStoreClasses;
using System.Collections.Generic;
using System.Web.Http;

namespace BookstoreAPI.Controllers
{
    public class SearchController : ApiController
    {
        //Used to receive strings from frontend using json
        public class SearchMessage
        {
            public string SearchString { get; set; }
        }

        // POST api/search
        public int Post(SearchMessage msg)
        {
            if (msg != null && msg.SearchString != null)
            {
                return BookstoreConnection.Instance.StartTask(BookstoreConnection.Instance.Service.GetBooksAsync(msg.SearchString));
            }
            return 0;
        }

        // GET api/search
        public IEnumerable<IBook> Get(int id)
        {
            if (id == 0)
                return null;
            var task = BookstoreConnection.Instance.GetTask(id);
            if (task == null)
                return null;
            if (task.IsCompleted)
                return task.Result;
            return null;
        }
    }
}

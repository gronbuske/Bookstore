using BookStoreClasses;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace BookstoreAPI.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SearchController : ApiController
    {
        // GET api/search
        public async Task<IEnumerable<IBook>> Get([FromUri]string searchString)
        {
            var task = BookstoreConnection.Instance.Service.GetBooksAsync(searchString);
            task.Start();
            return await task;
        }
    }
}

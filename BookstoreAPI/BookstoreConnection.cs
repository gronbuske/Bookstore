using BookStoreClasses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreAPI
{
    //Singleton object of BookStoreService
    public class BookstoreConnection
    {
        private IBookstoreService service;
        private static BookstoreConnection instance;

        private BookstoreConnection()
        {
        }

        public static BookstoreConnection Instance
        {
            get
            {
                if(instance == null)
                    instance = new BookstoreConnection();
                return instance;
            }
        }

        public IBookstoreService Service
        {
            get
            {
                return service;
            }
        }

        public void SetService(IBookstoreService service)
        {
            this.service = service;
        }
    }
}
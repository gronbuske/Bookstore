using BookStoreClasses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreAPI
{
    public class BookstoreConnection
    {
        private IBookstoreService service;
        private Dictionary<int, Task<IEnumerable<IBook>>> tasks;
        int taskCounter;
        private static BookstoreConnection instance;

        private BookstoreConnection()
        {
            tasks = new Dictionary<int, Task<IEnumerable<IBook>>>();
            taskCounter = 1;
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

        public int StartTask(Task<IEnumerable<IBook>> task)
        {
            int temp = taskCounter++;
            tasks[temp] = task;
            tasks[temp].Start();
            return temp;
        }

        //Returns task with id id
        public Task<IEnumerable<IBook>> GetTask(int id)
        {
            if (id == 0 || tasks.ContainsKey(id) == false)
                return null;
            return tasks[id];
        }

        public void SetService(IBookstoreService service)
        {
            this.service = service;
        }
    }
}
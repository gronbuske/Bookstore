using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStoreImplementation;

namespace BookStore_UnitTests
{
    [TestClass]
    public class BookStoreTests
    {
        Random rand;
        BookstoreService service;

        [TestInitialize]
        public void TestInitialization()
        {
            service = new BookstoreService();
            rand = new Random(1337);
        }

        [TestMethod]
        public void RandomSearchstrings()
        {
            for (int i = 0; i < 1000; i++)
            {
                byte[] searchStringByteArray = new byte[2048];
                rand.NextBytes(searchStringByteArray);
                string searchString = System.Text.Encoding.UTF8.GetString(searchStringByteArray);
                var task = service.GetBooksAsync(searchString);
                task.Start();
                task.Wait(1000);
                Assert.IsTrue(task.IsCompleted);
            }
        }

        [TestMethod]
        public void FindSpecificBooks()
        {
            var task = service.GetBooksAsync("ä");
            task.Start();
            task.Wait(300);
            int counter = 0;
            foreach(var book in task.Result)
            {
                counter++;
                Book b = (Book)book;
                Assert.AreEqual("Average Swede", b.Author);
                Assert.AreEqual("Mastering åäö", b.Title);
                Assert.AreEqual(762, b.Price);
                Assert.AreEqual(15, b.InStock);
            }
            Assert.AreEqual(1, counter);

            task = service.GetBooksAsync("How:Spend");
            task.Start();
            task.Wait(300);
            counter = 0;
            foreach (var book in task.Result)
            {
                counter++;
                Book b = (Book)book;
                Assert.AreEqual("Rich Block", b.Author);
                Assert.AreEqual("How To Spend Money", b.Title);
                Assert.AreEqual(1000000, b.Price);
                Assert.AreEqual(1, b.InStock);
            }
            Assert.AreEqual(1, counter);

            task = service.GetBooksAsync("ThOr");
            task.Start();
            task.Wait(300);
            counter = 0;
            foreach (var book in task.Result)
            {
                counter++;
                Book b = (Book)book;
                Assert.AreEqual("Generic Title", b.Title);
                Assert.IsTrue("First Author" == b.Author || "Second Author" == b.Author);
                Assert.IsTrue((Decimal)185.5 == b.Price || 1748 == b.Price);
                Assert.IsTrue(b.InStock == 5 || b.InStock == 3);
            }
            Assert.AreEqual(2, counter);

            task = service.GetBooksAsync("andom");
            task.Start();
            task.Wait(300);
            counter = 0;
            foreach (var book in task.Result)
            {
                counter++;
                Book b = (Book)book;
                Assert.AreEqual("Random Sales", b.Title);
                Assert.AreEqual("Cunning Bastard", b.Author);
                Assert.IsTrue((Decimal)499.5 == b.Price || 999 == b.Price);
                Assert.IsTrue(b.InStock == 20 || b.InStock == 3);
            }
            Assert.AreEqual(2, counter);

            task = service.GetBooksAsync("bloke+poor undesired-onödiga.ord");
            task.Start();
            task.Wait(300);
            counter = 0;
            foreach (var book in task.Result)
            {
                counter++;
                Book b = (Book)book;
                Assert.AreEqual("Rich Bloke", b.Author);
                Assert.AreEqual("Desired", b.Title);
                Assert.AreEqual((Decimal)564.5, b.Price);
                Assert.AreEqual(0, b.InStock);
            }
            Assert.AreEqual(1, counter);
        }
    }
}

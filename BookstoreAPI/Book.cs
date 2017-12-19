using BookStoreClasses;
using System;
using System.Runtime.Serialization;

namespace BookStoreImplementation
{
    [DataContract]
    public class Book : IBook
    {
        private Guid id;
        private string title;
        private string author;
        private decimal price;
        private int inStock;

        [DataMember]
        public string Title{ get{ return title; } set { title = value; } }
        [DataMember]
        public string Author{ get{ return author; } set { author = value; } }
        [DataMember]
        public decimal Price{ get{ return price; } set { price = value; } }
        [DataMember]
        public int InStock { get { return inStock; } set { inStock = value; } }

        //Unique ID for this book, is not uncluded in a serialization of the book
        public Guid ID{ get { return ID; } }

        public Book()
        {
            id = Guid.NewGuid();
            title = "";
            author = "";
            price = 0;
            inStock = 0;
        }

        public Book(string title, string author, decimal price, int inStock)
        {
            this.title = title;
            this.author = author;
            this.price = price;
            this.inStock = inStock;
            id = Guid.NewGuid();
        }
    }
}

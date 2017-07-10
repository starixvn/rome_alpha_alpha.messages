using System;

namespace Alpha.Messages.Models
{
    public class Product
    {
        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Product(int id, string name, DateTime endOfSupportDate) : this(id, name)
        {
            EndOfSupportDate = endOfSupportDate;
        }

        public int Id { get; }

        public string Name { get; }

        private DateTime EndOfSupportDate { get; set; }
    }
}

namespace Alpha.Messages.Models
{
    using System;

    public class Product
    {
        public Product(int id, string name, DateTime endOfSupportDate)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; }
    }
}

namespace Alpha.Messages.Models.Product
{
    public class Product
    {
        public Product()
        {
        }

        public Product(int id, string name, bool isHidden)
        {
            Id = id;
            Name = name;
            IsHidden = isHidden;
        }

        public int Id { get; }

        public string Name { get; }

        public bool IsHidden { get; }
    }
}
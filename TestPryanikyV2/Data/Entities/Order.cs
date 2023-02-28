namespace TestPryanikyV2.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}

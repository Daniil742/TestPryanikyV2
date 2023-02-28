namespace TestPryanikyV2.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}

namespace MyCompany.SharedCode
{
    public interface IProductRepository
    {
        Product GetProduct(int id);
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
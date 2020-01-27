using WebScrapping.Data;

namespace WebScrapping.Services
{
    public interface IProductRepository
    {
        void AddProduct(WebData product);
    }
}
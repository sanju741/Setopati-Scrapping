using System;
using System.Collections.Generic;
using System.Text;
using WebScrapping.Data;

namespace WebScrapping.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;
        public ProductRepository(Context context)
        {
            _context = context;
        }

        public void AddProduct(WebData product)
        {
            _context.WebData.Add(product);
            _context.SaveChanges();
        }
    }
}

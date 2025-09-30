using Shelf.DataAccess.Data;
using Shelf.DataAccess.Repository.IRepository;
using Shelf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelf.DataAccess.Repository
{
    public class ProductRepository : Repository<Product> , IProductRepository
    {
        private readonly ApplicationDBContext _db;
        public ProductRepository(ApplicationDBContext db) : base(db) 
        {
            _db = db;        
        }
        public void Update(Product product)
        {
            _db.Products.Update(product);
        }
    }
}

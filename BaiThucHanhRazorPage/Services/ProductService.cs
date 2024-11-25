using BaiThucHanhRazorPage.Data;
using BaiThucHanhRazorPage.Models;
using Microsoft.EntityFrameworkCore;

namespace BaiThucHanhRazorPage.Services
{
    public class ProductService
    {

        

private readonly QlbanhangContext _context;
        public ProductService(QlbanhangContext context)
        {
            _context = context;
        }

        public List<Product> GetProducts() { return _context.Products.ToList(); }

        public Product GetProductById(int id) {
            
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        //public List<Product> LoadProducts()
        //{
        //    products = new List<Product>()
        //    {
        //        new Product {Id=1, Name = "Iphone 14 Pro", Description = "Điện thoại Apple", Price = 1000 },
        //        new Product {Id=2, Name = "Samsung Galaxy", Description = "Điện thoại Samsung", Price = 500 },
        //        new Product {Id=3, Name = "Sony Xperia", Description = "Điện thoại Sony", Price = 800 }
        //    };
        //    return products;
        //}

        public void AddProducts(Product product)
        {
            ////Gán ID tự động
            //int newId = products.Any() ? products.Max(p => p.Id) + 1 : 1;
            //product.Id = newId;

            //Thêm SP
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public List<Product> SearchProduct(string name)
        {
            return _context.Products.Where(p => EF.Functions.Like(p.Name, $"%{name}%")).ToList();
            //return products.Where(p => p.Name.Contains(name, System.StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void RemoveProducts()
        {
            _context.Products.RemoveRange(_context.Products);
            _context.SaveChanges();
            //products.Clear();
        }

        public void RemoveSingleProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p =>p.Id == id);

            if(product != null)
            {
                //Xóa sản phẩm
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Không tìm thấy sản phẩm");
            }
        }

        public void LoadDefaults()
        {
            var defaultProducts = new List<Product>()
            {
                new Product {Name = "Iphone 14 Pro", Description = "Điện thoại Apple", Price = 1000 },
                new Product {Name = "Samsung Galaxy S23", Description = "Điện thoại Samsung", Price = 800 },
                new Product {Name = "Sony Xperia 1", Description = "Điện thoại Sony", Price = 700 }
            };
            foreach (var items in defaultProducts)
            {
                _context.Products.Add(items);
                
            }

            _context.SaveChanges();
        }

        internal void UpdateProduct(Product updatedProduct)
        {
            //Tìm sản phẩm trong danh sách id
            var existingProduct = _context.Products.FirstOrDefault(p => p.Id == updatedProduct.Id);

            if(existingProduct != null)
            {
                //Cập nhật thông tin
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Description = updatedProduct.Description;
                existingProduct.Price = updatedProduct.Price;

                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Sản phẩm không có");
            }
        }

        public void ResetProducts()
        {
            //Xóa toàn bộ dữ liệu trong bảng
            _context.Products.RemoveRange(_context.Products);
            _context.SaveChanges();

            //Thiết lập lại IDENTITY của bảng
            _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Products', RESEED, 0)");
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }
    }

}

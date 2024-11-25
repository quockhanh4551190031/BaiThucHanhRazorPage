using Microsoft.AspNetCore.Mvc;
using BaiThucHanhRazorPage.Models;
using BaiThucHanhRazorPage.Services;

namespace BaiThucHanhRazorPage.Pages.Components.ProductBox
{
    public class ProductBox : ViewComponent
    {
        /*List<Product> products = new List<Product>()
        {
            new Product(){Name = "Iphone 14 Pro", Description = "Điện thoại Apple", Price = 1000},
            new Product(){Name = "Samsung Galaxy", Description = "Điện thoại Samsung", Price = 500},
            new Product(){Name = "Sony Xperia", Description = "Điện thoại Sony", Price = 800}
        };*/

        //Service
        List<Product> products = null;
        public ProductBox(ProductService productService)
        {
            products = productService.GetProducts();
        }
        

        /*public IViewComponentResult Invoke()
        {
            //return View()
            return View<List<Product>>(products); // Default.cshtml
        }*/


        //Trường hợp Invoke có thêm tham số
        public async Task<IViewComponentResult> InvokeAsync(bool sapxeptang = true)
        {
            List<Product> _products = null;
            if(sapxeptang)
                _products = products.OrderBy(p => p.Price).ToList();
            else
                _products = products.OrderByDescending(p => p.Price).ToList();
            //return View()
            return View<List<Product>>("Default",_products); //Default.cshtml
        }
    }
}

using BaiThucHanhRazorPage.Models;
using BaiThucHanhRazorPage.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace BaiThucHanhRazorPage.Pages
{
    public class ProductPageModel : PageModel
    {
        public Product product;
        private readonly ProductService _productService;  
        public List<Product> products { get; set; }
        public List<Product> FilteredProducts { get; set; } //Danh sách tìm

        [BindProperty(SupportsGet =true)]
        public string SearchQuery { get; set; }

       

        public ProductPageModel(ProductService productService)
        {
            
            _productService = productService;

        }
        public void OnGet(int? id)
        {
            products = _productService.GetProducts();

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                //Lọc sản phẩm dựa trên tìm
                FilteredProducts = products
                    .Where(p=>p.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();

            }

            if (id != null)
            {
                ViewData["Title"] = $"Thông tin sản phẩm (ID={id.Value})";
                product = _productService.GetProductById(id.Value);
            }
            else
            {
                ViewData["Title"] = $"Danh sách sản phẩm";
            }

        }

        public IActionResult OnGetLastProduct()
        {
            ViewData["Title"] = $"Sản phẩm cuối";
            product = _productService.GetProducts().LastOrDefault();
            if (product != null) { return Page(); }
            return NotFound();
        }

        public IActionResult OnGetRemoveAll()
        {
            _productService.RemoveProducts();
            return RedirectToPage();
        }

        public IActionResult OnGetLoadAll()
        {
            _productService.LoadDefaults();
            return RedirectToPage("ProductPage");
        }

        public IActionResult OnGetRemove(int id)
        {
            _productService.RemoveSingleProduct(id);
            return RedirectToPage("ProductPage");
        }

        public IActionResult OnGetCreate()
        {
            return RedirectToPage("CreateProductPage");
        }

        public IActionResult OnGetResetProducts()
        {
            _productService.ResetProducts();
            return RedirectToPage();
        }
        
    }
}

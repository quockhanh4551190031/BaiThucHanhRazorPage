using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BaiThucHanhRazorPage.Models;
using BaiThucHanhRazorPage.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaiThucHanhRazorPage.Pages
{
    public class CreateProductPageModel : PageModel
    {
        private static ProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public List<IFormFile> Images { get; set; }
        public List<Product> _products { get; set; }
        public List<Product> FilteredProducts { get; set; }

        public CreateProductPageModel(ProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }

        // Phương thức GET hiển thị form tạo
        public IActionResult OnGet()
        {
            return Page();
        }

        // Phương thức POST để lưu SP
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["titleContent"] = "ERROR";
                ViewData["content"] = "Dữ liệu không hợp lệ";
                return Page();
            }

            // Kiểm tra thư mục images 
            var imagesPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            if(!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
            }

            var imagePaths = new List<string>();
            foreach(var image in Images)
            {
                if(image.Length > 0)
                {
                    var filePath = Path.Combine(imagesPath, image.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    imagePaths.Add("/images/" + image.FileName);
                }
            }
            //Lưu danh sách đường dẫn ảnh vào Product
            Product.PathImages = imagePaths;

            // Thêm sản phẩm vào danh sách
            _productService.AddProducts(Product);

            // Chuyển trang
            return RedirectToPage("ProductPage");
        }
    }
}

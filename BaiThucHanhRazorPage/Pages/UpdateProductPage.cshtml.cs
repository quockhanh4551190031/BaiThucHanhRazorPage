using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BaiThucHanhRazorPage.Models;
using BaiThucHanhRazorPage.Services;

namespace BaiThucHanhRazorPage.Pages
{
    public class UpdateProductPageModel : PageModel
    {
        private readonly ProductService productService;
        private readonly IWebHostEnvironment _env;

        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public List<IFormFile> Images { get; set; }

        public UpdateProductPageModel(ProductService productServices, IWebHostEnvironment env)
        {
            productService = productServices;
            _env = env;
          
        }
        public IActionResult OnGet(int id)
        {
            Product = productService.GetProductById(id);

            if(Product == null)
            {
                return RedirectToPage("ProductPage");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();

            }

            //Cập nhật SP
            var existingProduct = productService.GetProductById(id);
            if(existingProduct == null)
            {
                return RedirectToPage("ProductPage");
            }

            //Cập nhật thông tin
            existingProduct.Name = Product.Name;
            existingProduct.Description = Product.Description;
            existingProduct.Price = Product.Price;

            //Kiểm tra và sửa ảnh
            var imagePath = Path.Combine(_env.WebRootPath, "images");
            if(!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            foreach (var image in Images)
            {
                if(image.Length > 0)
                {
                    var filePath = Path.Combine(imagePath, image.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    imagePaths.Add("/images/" + image.FileName);
                }
            }

            Product.PathImages = imagePaths;

            //Lưu & Cập nhật SP
            productService.UpdateProduct(existingProduct);

            return RedirectToPage("ProductPage");
        }
    }
}

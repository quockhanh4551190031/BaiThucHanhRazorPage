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

            // Tìm sản phẩm theo ID
            var existingProduct = productService.GetProductById(id);
            if (existingProduct == null)
            {
                return RedirectToPage("ProductPage");
            }

            // Cập nhật thông tin sản phẩm (tên, mô tả, giá)
            existingProduct.Name = Product.Name;
            existingProduct.Description = Product.Description;
            existingProduct.Price = Product.Price;

            // Xử lý ảnh
            var imagePath = Path.Combine(_env.WebRootPath, "images");
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            // Xóa ảnh cũ
            var oldImagePaths = existingProduct.PathImages ?? new List<string>();
            foreach (var oldImagePath in oldImagePaths)
            {
                var fullOldImagePath = Path.Combine(_env.WebRootPath, oldImagePath.TrimStart('/'));
                if (System.IO.File.Exists(fullOldImagePath))
                {
                    System.IO.File.Delete(fullOldImagePath); // Xóa ảnh cũ
                }
            }

            // Thêm ảnh mới
            var newImagePaths = new List<string>();
            foreach (var image in Images)
            {
                if (image.Length > 0)
                {
                    var filePath = Path.Combine(imagePath, image.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    newImagePaths.Add("/images/" + image.FileName); // Đường dẫn mới
                }
            }

            // Gán lại đường dẫn ảnh mới vào sản phẩm
            existingProduct.PathImages = newImagePaths;

            // Lưu cập nhật sản phẩm
            productService.UpdateProduct(existingProduct);

            return RedirectToPage("ProductPage");
        }

    }
}

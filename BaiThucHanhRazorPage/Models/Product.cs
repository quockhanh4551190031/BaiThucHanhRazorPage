
using System.ComponentModel.DataAnnotations;


namespace BaiThucHanhRazorPage.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [MinLength(5, ErrorMessage = "Tên sản phẩm phải ít nhất 5 ký tự")]
        [MaxLength(100, ErrorMessage = "Tên sản phẩm không được quá 100 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mô tả sản phẩm là bắt buộc")]
        [MinLength(5, ErrorMessage = "Mô tả phải ít nhất 5 ký tự")]
        [MaxLength(500, ErrorMessage = "Mô tả không được quá 500 ký tự")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải là số")]
        public Decimal Price { get; set; } = 0;

        //Thuộc tích upload ảnh (Danh sách ảnh)
        [Required(ErrorMessage = "Sản phẩm phải có ảnh")]
        public List<string> PathImages { get; set; } = new List<string>();   
    }
}

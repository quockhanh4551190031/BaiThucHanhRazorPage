﻿namespace BaiThucHanhRazorPage.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }

        public ICollection<Product> SanPham { get; set; }
    }
}

using BaiThucHanhRazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaiThucHanhRazorPage.Pages
{
    public class ContactPageModel : PageModel
    {
        [BindProperty]
        public Contact contact { get; set; }

        public ContactPageModel() 
        {
            contact = new Contact();
        }
        public string thongbao { get; set; }
        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                thongbao = "Dữ liệu gửi đến hợp lệ";
            }
            else
            {
                thongbao = "Dữ liệu không hợp lệ";
            }
        }
        public void OnGet()
        {
        }
    }
}

using BaiThucHanhRazorPage.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BaiThucHanhRazorPage.Models
{
    public class Contact
    {
        [BindProperty]
        [DisplayName("Id của bạn")]
        [Range(1,100,ErrorMessage = "Nhập sai")]

        public int ContactId { get; set; }
        [BindProperty]

        public string FirstName {  get; set; }
        [BindProperty]

        public string LastName { get; set; }


        [BindProperty]
        [DataType(DataType.Date)]
        //[CustomBirthDate(ErrorMessage = "Ngày sinh nhỏ hơn hoặc bằng ngày hiện tại")

        public DateTime DateOfBirth { get; set; }

        [BindProperty]
        [EmailAddress(ErrorMessage = "Nhập sai định dạng")]

        public string Email {  get; set; }
    }
}

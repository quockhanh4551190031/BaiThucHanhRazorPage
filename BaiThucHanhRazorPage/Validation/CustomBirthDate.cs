using System.ComponentModel.DataAnnotations;

namespace BaiThucHanhRazorPage.Validation
{
    public class CustomBirthDate : ValidationAttribute
    {
        public CustomBirthDate()
            :base("Ngày sinh phải nhỏ hơn hoặc bằng ngày hiện tại")
        {

        }

        public override bool IsValid(object value)
        {
            if(value is DateTime birthDate)
            {
                return birthDate <= DateTime.Now;
            }
            return false;
        }
    }
}

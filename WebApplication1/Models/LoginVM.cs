using System.ComponentModel.DataAnnotations;

namespace WebApplication1.PresentationLayer.Models
{
    public class LoginVM
    {
        [
            Required(ErrorMessage = "lütfen mail adresi giriniz"),
            DataType(DataType.EmailAddress),
            UIHint("email")
        ]
        public string Email { get; set; }


        [
            Required(ErrorMessage = "lütfen sifrenizi giriniz"),
            DataType(DataType.Password),
            UIHint("password")
        ]
        public string Password { get; set; }
    }
}

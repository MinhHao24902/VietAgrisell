using System.ComponentModel.DataAnnotations;

namespace VietAgrisell.ViewModels
{
    public class LoginViewModel
    {
        [Display (Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Chưa nhập tên đăng nhập")]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Chưa nhập mật khẩu")]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}

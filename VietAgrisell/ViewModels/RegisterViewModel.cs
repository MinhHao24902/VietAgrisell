using System.ComponentModel.DataAnnotations;

namespace VietAgrisell.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "*")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        [Display(Name = "Họ tên")]
        public string Name { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        [RegularExpression(@"0[39]\d{8}", ErrorMessage = "Vui lòng nhập định dạng số điện thoại Việt Nam")]
        public string Mobile { get; set; }

        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng email")]
        public string Email { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        public DateTime DateOfBirth { get; set; }

        public bool Gender {  get; set; }

        public string? ImageUrl { get; set; }

        public int Role { get; set; }

        public bool Available { get; set; }
    }
}

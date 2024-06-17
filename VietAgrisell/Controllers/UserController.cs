using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VietAgrisell.Data;
using VietAgrisell.Helpers;
using VietAgrisell.ViewModels;

namespace VietAgrisell.Controllers
{
    public class UserController : Controller
    {
        private readonly VietAgrisellContext db;
        private readonly IMapper _mapper;

        public UserController(VietAgrisellContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        //IFormFile Pic
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User();
                //var user = _mapper.Map<User>(model);
                user.RandomKey = MyUtil.GenerateRandomKey();
                user.UserName = model.UserName;
                user.Password = model.Password.ToMd5Hash(user.RandomKey);
                user.Address = model.Address;
                user.Email = model.Email;
                user.Mobile = model.Mobile;
                user.DateOfBirth = model.DateOfBirth;
                user.Gender = model.Gender;
                user.Name = model.Name;
                user.Password = model.Password;
                user.Available = true;
                user.Role = 0;
                user.ImageUrl = null;
                //if (Pic != null)
                //{
                //    user.ImageUrl = MyUtil.UploadPicture(Pic, "User");
                //}
                db.Add(user);
                db.SaveChanges();
                return RedirectToAction("Profile");
            }
            else
            {
                ModelState.AddModelError("New Error", "Invalid Data");
            }
            return View();
        }
        #endregion Register

        #region Login
        [HttpGet]
        public IActionResult Login(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u =>
                    u.UserName == model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("Lỗi", "Khách hàng không tồn tại");
                }
                else
                {
                    if (!user.Available)
                    {
                        ModelState.AddModelError("Lỗi", "Tài khoản đã vị khoá. Vui lòng liên hệ Admin");
                    }
                    else
                    {
                        if (user.Password != model.Password)
                        //if (user.Password != model.Password.ToMd5Hash(user.RandomKey))
                        {
                            ModelState.AddModelError("Lỗi", "Sai mật khẩu");
                        }
                        else
                        {
                            var claims = new List<Claim> {
                                new Claim(ClaimTypes.Email, user.Email),
                                new Claim(ClaimTypes.Name, user.Name),
                                new Claim(MySetting.CLAIM_CUSTOMERNAME, user.UserName),
                                new Claim(ClaimTypes.Role, "Customer"),
                                };

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                            await HttpContext.SignInAsync(claimsPrincipal);

                            if(Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else
                            {
                                return Redirect("Profile");
                            }
                        }
                    }
                }
            }
            return View();
        }
        #endregion

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("Profile");
        }
    }
}

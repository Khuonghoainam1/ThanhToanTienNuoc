using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThanhToanTienNuoc.Data;
using ThanhToanTienNuoc.Models;

namespace ThanhToanTienNuoc.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin!";
                return View();
            }

            var admin = await _context.AdminAccounts
                .FirstOrDefaultAsync(a => a.Username == username && a.IsActive ==true && a.PasswordHash == password);

           /* if (admin == null || !VerifyPassword(password, admin.PasswordHash))
            {
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng!";
                return View();
            }
*/
            // Lưu thông tin đăng nhập (phiên làm việc)
            HttpContext.Session.SetString("Username", admin.Username);
            return RedirectToAction("Index", "NguoiDung"); // Chuyển đến trang quản lý khách hàng
        }
        public IActionResult ManageWaterUsers()
        {
            return View();
        }


        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            // So sánh hash của mật khẩu nhập vào với hash đã lưu
            string inputHash = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputPassword)));
            return inputHash == storedHash;
        }
    }
}
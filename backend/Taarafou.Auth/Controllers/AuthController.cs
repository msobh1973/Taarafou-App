using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Taarafou.Auth.Models;

namespace Taarafou.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // دالة تسجيل الدخول
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // تحقق من صحة بيانات الاعتماد (البريد الإلكتروني وكلمة المرور)
            var user = ValidateUserCredentials(request.Email, request.Password);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // توليد رمز JWT للمستخدم
            var token = GenerateJwtToken(user);

            // إرسال البريد الإلكتروني بعد تسجيل الدخول بنجاح
            SendLoginEmail(user.Email);

            return Ok(new { token });
        }

        // دالة تسجيل المستخدم الجديد
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            // من هنا تبدأ عملية إنشاء حساب جديد للمستخدم
            return Ok(new { message = "تم إنشاء الحساب بنجاح." });
        }

        // دالة التحقق من بيانات المستخدم
        private User ValidateUserCredentials(string email, string password)
        {
            // هنا يمكن إضافة منطق التحقق من بيانات المستخدم من قاعدة البيانات
            // في الوقت الحالي، سنعيد كائن اختبار لمستخدِم افتراضي
            if (email == "msobh73@gmail.com" && password == "password123")
            {
                return new User { Email = email, FullName = "محمد صبح" };
            }
            return null;
        }

        // دالة لتوليد رمز JWT
        private string GenerateJwtToken(User user)
        {
            // من هنا يتم توليد رمز JWT باستخدام بيانات المستخدم
            return "generated-jwt-token";  // هذا مثال. يجب استبداله بمنطق التوليد الفعلي
        }

        // دالة لإرسال البريد الإلكتروني عند تسجيل الدخول بنجاح
        private void SendLoginEmail(string userEmail)
        {
            var smtpClient = new SmtpClient(_configuration["SmtpSettings:Host"])
            {
                Port = int.Parse(_configuration["SmtpSettings:Port"]),
                Credentials = new NetworkCredential(_configuration["SmtpSettings:Username"], _configuration["SmtpSettings:Password"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["SmtpSettings:Username"]),
                Subject = "Login Successful",
                Body = $"Hello, you have successfully logged in!",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(userEmail);

            smtpClient.Send(mailMessage);
        }
    }

    // كائن المستخدم
    public class User
    {
        public string Email { get; set; }
        public string FullName { get; set; }
    }

    // كائن البيانات الخاصة بتسجيل الدخول
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    // كائن البيانات الخاصة بتسجيل المستخدم الجديد
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

using APIBank.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly DbBankContext _context;
        private readonly IMemoryCache _cache;
        private string cod;
        public AuthorizationController(DbBankContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }
        private static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        [HttpGet("Authorization")]
        public IActionResult Login(string userNumberPhone, string password)
        {
            string hashedPassword = ComputeSha256Hash(password);
            var user = _context.Users.FirstOrDefault(x => x.UserNumberPhone == userNumberPhone && x.UserPassword == hashedPassword);
            if (user == null)
            {
                return BadRequest();
            }
            var json = JsonConvert.SerializeObject(user, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("elisei.pavlenko12@mail.ru", "CifroBank");

            Random random = new Random();
            cod = random.Next(1000, 9999).ToString();
            // Сохранение кода в кэше на 5 минут
            _cache.Set(userNumberPhone, cod, TimeSpan.FromMinutes(5)); 
            // кому отправляем
            MailAddress to = new MailAddress($"{user.UserEmail}");
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма Заголовок
            m.Subject = "Код потвреждения";

            

            // текст письма
            m.Body = $"<h2>Ваш код потверждения: {cod}</h2>";

            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("elisei.pavlenko12@mail.ru", "E8YSawVmkTehYU678CeZ");
            smtp.EnableSsl = true;
            smtp.Send(m);

            return Ok($"{user.UserRoleId}");
        }
        [HttpGet("IdentityVerification")]
        public IActionResult IdentityVerification(string userNumberPhone, string cod)
        {
            if (_cache.TryGetValue(userNumberPhone, out string cachedCod) && cachedCod == cod)
            {
                return Ok("Код подтвержден");   
            }
            else
            {
                return BadRequest("Неверный код или истек срок действия.");
            }
        }
        [HttpPost("AddUser")]
        public IActionResult AddUser(string userNumberPhone, string email, string fio, string password, DateTime bistday)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                password = builder.ToString();
            }
            DateOnly date = new DateOnly(bistday.Year, bistday.Month, bistday.Day);
            StringBuilder errors = new StringBuilder();
            foreach (var user in _context.Users)
            {
                if (user.UserNumberPhone == userNumberPhone)
                {
                     errors.AppendLine("Данный номер телефона зарегистрирован");
                }
                if (user.UserEmail == email)
                {
                    errors.AppendLine("Данная почта уже зарегистрирована");
                }
            }
            if (errors.Length > 0)
            {
                return BadRequest(errors.ToString());
            }
            User client = new User
            {
                UserNumberPhone = userNumberPhone,
                UserEmail = email,
                UserFio = fio,
                UserPassword = password,
                UserBirthday = date,
                UserRoleId = 1
            };
            _context.Users.Add(client);
            _context.SaveChanges();
            return Ok();
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShop.Data;
using MyShop.Models;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net; // ��� ����������� �������

namespace MyShop.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public RegisterModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var lastname = Request.Form["lastname"].ToString();
            var firstname = Request.Form["firstname"].ToString();
            var login = Request.Form["login"].ToString();
            var password = Request.Form["password"].ToString();
            var policy = Request.Form["policy"].ToString() == "on";

            if (string.IsNullOrWhiteSpace(lastname) ||
                string.IsNullOrWhiteSpace(firstname) ||
                string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "��� ���� �����������.");
                return Page();
            }

            // �������� ������������ ������
            if (_db.ApplicationUser.Any(u => u.Login == login))
            {
                ModelState.AddModelError("", "������������ � ����� ������� ��� ����������.");
                return Page();
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new ApplicationUser
            {
                LastName = lastname,
                FirstName = firstname,
                Login = login,
                PasswordHash = passwordHash,
                PolicyAgreed = policy
            };

            _db.ApplicationUser.Add(user);
            await _db.SaveChangesAsync();

            // ����� ����������� ����� ����� ���������� ������������ ��� ��������� ����� �������
            return RedirectToPage("/Login");
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShop.Data;
using MyShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _db;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        // Свойство для продуктов, которое будет передано в представление
        public List<Product> Products { get; set; }

        // Метод для обработки GET-запросов
        public void OnGet()
        {
            // Загружаем список продуктов из базы данных
            Products = _db.Products.ToList();
        }

        // Метод для обработки POST-запроса при выходе из аккаунта
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            // Удаляем аутентификационную куку
            await HttpContext.SignOutAsync("MyCookieAuth");

            // Перенаправляем пользователя на главную страницу
            return RedirectToPage("/Index");
        }
    }
}

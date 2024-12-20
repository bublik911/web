using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShop.Data;
using MyShop.Models;
using MyShop.Utils;

namespace MyShop.Pages.products
{
    public class ProductDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Product Product { get; set; }
        public string ProductDescription { get; set; }

        public ProductDetailsModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet(int id)
        {
            // Ищем товар по ID
            Product = _db.Products.FirstOrDefault(p => p.Id == id);

            if (Product == null)
            {
                return NotFound();
            }

            ProductDescription = "werwer";
            return Page();
        }

        // Обработчик для кнопки "Добавить в корзину"
        public IActionResult OnPostAddToCart(int productId)
        {
            // Находим товар в базе данных
            var product = _db.Products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                return NotFound();
            }

            // Создаем элемент корзины
            var cartItem = new CartItems
            {
                ProductId = product.Id,
                Name = product.Name,
                Quantity = 1,
                Price = product.Price
            };

            // Добавляем товар в корзину (сессия)
            CartHelper.AddToCart(HttpContext.Session, cartItem);

            // Перенаправляем на страницу корзины
            return RedirectToPage("/Catalog");
        }
    }
}

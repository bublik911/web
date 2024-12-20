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

        // �������� ��� ���������, ������� ����� �������� � �������������
        public List<Product> Products { get; set; }

        // ����� ��� ��������� GET-��������
        public void OnGet()
        {
            // ��������� ������ ��������� �� ���� ������
            Products = _db.Products.ToList();
        }

        // ����� ��� ��������� POST-������� ��� ������ �� ��������
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            // ������� ������������������ ����
            await HttpContext.SignOutAsync("MyCookieAuth");

            // �������������� ������������ �� ������� ��������
            return RedirectToPage("/Index");
        }
    }
}

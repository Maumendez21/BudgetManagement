using ManagementBudget.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManagementBudget.Controllers
{
    public class TypeAcountController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TypeAcountModel typeAcount)
        {
            if (!ModelState.IsValid)
            {
                return View(typeAcount);    
            }
            return View();
        }
    }
}

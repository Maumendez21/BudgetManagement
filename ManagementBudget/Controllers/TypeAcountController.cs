using Dapper;
using ManagementBudget.Models;
using ManagementBudget.Services;
using ManagementBudget.Services.TypeAcountService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ManagementBudget.Controllers
{
    public class TypeAcountController : Controller
    {
        private readonly IRepositoryTypeAcount repositoryTypeAcount;
        private readonly IUserService userService;

        public TypeAcountController(
            IRepositoryTypeAcount repositoryTypeAcount,
            IUserService userService
        )
        {
            this.repositoryTypeAcount = repositoryTypeAcount;
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = userService.GetUserId();
            var typesAcounts = await repositoryTypeAcount.Get(userId);
            return View(typesAcounts);
        }

        public IActionResult Create()
        {  
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TypeAcountModel typeAcount)
        {
            if (!ModelState.IsValid)
            {
                return View(typeAcount);    
            }
            typeAcount.UserId = userService.GetUserId();
            var exist = await repositoryTypeAcount.Exists(typeAcount.Name, typeAcount.UserId);
            if (exist)
            {
                ModelState.AddModelError(nameof(typeAcount.Name), $"El nombre {typeAcount.Name} ya existe.");
                return View(typeAcount);
            }
            await repositoryTypeAcount.Create(typeAcount);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var userId = userService.GetUserId();
            var typeAcount = await repositoryTypeAcount.GetForId(Id, userId);
            if (typeAcount is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(typeAcount);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(TypeAcountModel typeAcount)
        {
            var userId = userService.GetUserId();
            var typeAcountExist = await repositoryTypeAcount.GetForId(typeAcount.Id, userId);
            if (typeAcountExist is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            await repositoryTypeAcount.Update(typeAcount);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int Id)
        {
            var userId = userService.GetUserId();
            var typeAcount = await repositoryTypeAcount.GetForId(Id, userId);
            if (typeAcount is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(typeAcount);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTypeAcount(int id)
        {
            var userId = userService.GetUserId();
            var typeAcount = await repositoryTypeAcount.GetForId(id, userId);
            if (typeAcount is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositoryTypeAcount.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> VerifyExistTypeAcount(string name)
        {
            var userId = userService.GetUserId();
            var exists = await repositoryTypeAcount.Exists(name, userId);
            if (exists)
            {
                return Json($"El nombre {name} ya existe");
            }

            return Json(true);
        }


    }
}

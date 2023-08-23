using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.FlowerBouquetRepository;
using DataAccess.CategoryRepository;
using DataAccess.SupplierRepository;
using WebApplication.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication.Pages.Flower
{
    public class EditModel : PageModel
    {
        private readonly IFlowerBouquetRepository flowerBouquetRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ISupplierRepository supplierRepository;
        public EditModel(IFlowerBouquetRepository flowerBouquetRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository)
        {
            this.flowerBouquetRepository = flowerBouquetRepository;
            this.categoryRepository = categoryRepository;
            this.supplierRepository = supplierRepository;
        }

        [BindProperty]
        public FlowerBouquet FlowerBouquet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                return RedirectToPage("/Welcome");
            }
            if (!HttpContext.Session.GetString("Role").Equals("Admin"))
            {
                return RedirectToPage("/Unauthen");
            }
            if (id == null || flowerBouquetRepository.GetById(id) == null)
            {
                return Redirect("/Index");
            }

            FlowerBouquet = (FlowerBouquet)flowerBouquetRepository.GetById(id);
            ViewData["CategoryId"] = new SelectList(categoryRepository.GetAll(), "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(supplierRepository.GetAll(), "SupplierId", "SupplierName");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (FlowerBouquet.UnitPrice < 0 || FlowerBouquet.UnitsInStock < 0 || FlowerBouquet.FlowerBouquetId <= 0)
            {
                ViewData["Nofication"] = "All your number information must be positive";
                ViewData["CategoryId"] = new SelectList(categoryRepository.GetAll(), "CategoryId", "CategoryName");
                ViewData["SupplierId"] = new SelectList(supplierRepository.GetAll(), "SupplierId", "SupplierName");
                return Page();
            }
            flowerBouquetRepository.Update(FlowerBouquet);
            return Redirect("./Index");
        }
    }
}

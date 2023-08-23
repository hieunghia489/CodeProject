using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.CategoryRepository;
using DataAccess.FlowerBouquetRepository;
using DataAccess.SupplierRepository;

namespace WebApplication.Pages.Flower
{
    public class DeleteModel : PageModel
    {
        private readonly IFlowerBouquetRepository flowerBouquetRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ISupplierRepository supplierRepository;
        public DeleteModel(IFlowerBouquetRepository flowerBouquetRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository)
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
                return Redirect("./Index");
            }

            FlowerBouquet = flowerBouquetRepository.GetById(id);
            FlowerBouquet.Category = categoryRepository.GetById(FlowerBouquet.CategoryId);
            FlowerBouquet.Supplier = supplierRepository.GetById(FlowerBouquet.SupplierId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null || flowerBouquetRepository.GetById(id) == null)
            {
                return Redirect("./Index");
            }
            flowerBouquetRepository.DeleteById(id);
            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using DataAccess.FlowerBouquetRepository;
using DataAccess.CategoryRepository;
using DataAccess.SupplierRepository;
using Microsoft.CodeAnalysis;
using WebApplication.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication.Pages.Flower
{
    public class CreateModel : PageModel
    {
        private readonly IFlowerBouquetRepository flowerBouquetRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ISupplierRepository supplierRepository;
        public CreateModel(IFlowerBouquetRepository flowerBouquetRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository)
        {
            this.flowerBouquetRepository = flowerBouquetRepository;
            this.categoryRepository = categoryRepository;
            this.supplierRepository = supplierRepository;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                return RedirectToPage("/Welcome");
            }
            if (!HttpContext.Session.GetString("Role").Equals("Admin"))
            {
                return RedirectToPage("/Unauthen");
            }
            ViewData["CategoryId"] = new SelectList(categoryRepository.GetAll(), "CategoryId", "CategoryName");    
        ViewData["SupplierId"] = new SelectList(supplierRepository.GetAll(), "SupplierId", "SupplierName");
            return Page();
        }

        [BindProperty]
        public FlowerBouquet FlowerBouquet { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            
            if ( FlowerBouquet == null)
            {
                ViewData["CategoryId"] = new SelectList(categoryRepository.GetAll(), "CategoryId", "CategoryName");
                ViewData["SupplierId"] = new SelectList(supplierRepository.GetAll(), "SupplierId", "SupplierName");
                return Page();
            }
          if(flowerBouquetRepository.GetById(FlowerBouquet.FlowerBouquetId)!=null)
            {
                ViewData["Nofication"] = "Duplicated ID";
                ViewData["CategoryId"] = new SelectList(categoryRepository.GetAll(), "CategoryId", "CategoryName");
                ViewData["SupplierId"] = new SelectList(supplierRepository.GetAll(), "SupplierId", "SupplierName");
                return Page();
            }
            if (FlowerBouquet.UnitPrice < 0 || FlowerBouquet.UnitsInStock < 0 || FlowerBouquet.FlowerBouquetId <= 0)
            {
                ViewData["Nofication"] = "All your number information must be positive";
                ViewData["CategoryId"] = new SelectList(categoryRepository.GetAll(), "CategoryId", "CategoryName");
                ViewData["SupplierId"] = new SelectList(supplierRepository.GetAll(), "SupplierId", "SupplierName");
                return Page();
            }

            flowerBouquetRepository.Save(FlowerBouquet);
                return RedirectToPage("./Index");
        }
    }
}

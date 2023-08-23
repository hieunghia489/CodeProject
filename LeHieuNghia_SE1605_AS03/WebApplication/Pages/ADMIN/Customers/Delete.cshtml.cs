using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.CustomerRepository;

namespace WebApplication.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;
        public DeleteModel(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }

        [BindProperty]
      public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                return RedirectToPage("/Welcome");
            }
            if (!HttpContext.Session.GetString("Role").Equals("Admin"))
            {
                return RedirectToPage("/Unauthen");
            }

            if (id == null || _customerRepository.GetById(id) == null)
            {
                ViewData["Nofication"] = "Customer doesn't exist";
                return Redirect("/");
            }

            var customer = _customerRepository.GetById(id);        
                Customer = customer;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null || _customerRepository.GetById(id)== null)
            {
                return Redirect("/Index");
            }
            _customerRepository.DeleteById(id);
            return RedirectToPage("./Index");
        }
    }
}

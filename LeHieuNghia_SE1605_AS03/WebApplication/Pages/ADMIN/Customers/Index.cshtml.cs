using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.CustomerRepository;
using System.Data;

namespace WebApplication.Pages.Customers
{

    public class IndexModel : PageModel
    {
        public IList<Customer> Customer { get; set; } = default;
        private readonly ICustomerRepository customerRepository;

        public IndexModel(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;

        }

        public async Task<IActionResult> OnGet()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                return RedirectToPage("/Welcome");
            }
            if (!HttpContext.Session.GetString("Role").Equals("Admin"))
            {
                return RedirectToPage("/Unauthen");
            }

            Customer = (IList<Customer>)customerRepository.GetAll();
                       return Page();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
/*using Microsoft.EntityFrameworkCore;*/
using BusinessObject.Models;
using DataAccess.CustomerRepository;
using DataAccess.OrderRepository;
using DataAccess.OrderDetailRepository;

namespace WebApplication.Pages.USER
{
    public class EditModel : PageModel
    {
        private readonly ICustomerRepository customerRepository;

        public EditModel(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
           
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                return RedirectToPage("/Welcome");
            }
            if (id == null || customerRepository.GetById(id)==null||id!=int.Parse( HttpContext.Session.GetString("ID")))
            {
                return Redirect("/");
            }

            Customer = customerRepository.GetById(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (!Customer.Email.Contains("@") || !Customer.Email.Contains("."))
            {
                ViewData["Nofication"] = "Your Email was not valid";
                return Page();
            }
            if(customerRepository.GetByEmail(Customer.Email) != null)
            { 
                    ViewData["Nofication"] = "This email existed already";
                    return Page();
            }
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
            string emailAdmin = config["AdminAccount:email"];
            if (Customer.Email.Equals(emailAdmin))
            {
                ViewData["Nofication"] = "This email is for admin already";
                return Page();
            }
            return RedirectToPage("./Index");
        }

    }
}

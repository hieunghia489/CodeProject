using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using DataAccess.CustomerRepository;

namespace WebApplication.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerRepository customerRepository;
        public CreateModel (ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository; 
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
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine(Customer.Birthday.ToString());
         if(Customer == null || !ModelState.IsValid)
            {
                ViewData["Nofication"] = "Your information is invalid";
                return Page();
            }
         if (customerRepository.GetById(Customer.CustomerId) != null)
            {
                ViewData["Nofication"] = " Duplicate ID";
                return Page();
            }
            if (!Customer.Email.Contains("@") ||!Customer.Email.Contains("."))
            {
                ViewData["Nofication"] = "Your Email was not valid";
                return Page();
            }
            if(customerRepository.GetByEmail(Customer.Email) != null)
            {
                ViewData["Nofication"] = "This email is used already";
                return Page();
            }
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
            string emailAdmin = config["AdminAccount:email"];
            if (Customer.Email.Equals(emailAdmin))
            {
                ViewData["Nofication"] = "This email is for admin already";
                return Page();
            }
            customerRepository.Save(Customer);
            return Redirect("./Index");
        }
    }
}

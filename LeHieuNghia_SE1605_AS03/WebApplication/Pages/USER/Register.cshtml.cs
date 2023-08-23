using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using DataAccess.CustomerRepository;
using System.Security.Cryptography;

namespace WebApplication.Pages
{ 
    public class Register : PageModel
    {
        private readonly ICustomerRepository customerRepository;
        public Register(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (Customer == null || !ModelState.IsValid)
            {
                ViewData["Nofication"] = "Your information is invalid";
                return Page();
            }

            if (!Customer.Email.Contains("@") || !Customer.Email.Contains("."))
            {
                ViewData["Nofication"] = "Your Email was not valid";
                return Page();
            }
            if (customerRepository.GetByEmail(Customer.Email) != null)
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
            int i = 0;
            while (customerRepository.GetById(i) != null)
            {
                i = Random.Shared.Next(0, 100);
            }
            Customer.CustomerId = i;
            customerRepository.Save(Customer);
            return Redirect("/");
        }
    }
}

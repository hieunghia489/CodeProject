using BusinessObject.DataTransferObject;
using BusinessObject.Models;
using DataAccess.CustomerRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication.Pages
{
    public class WelcomeModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;
        public WelcomeModel(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }
        [BindProperty]
      public AccountDTO Account { get; set; }
        HttpClient client=new HttpClient();
        public async Task<IActionResult> OnGet()
        {       
                if (HttpContext.Session.GetString("Role") != null)
                {
                    if (HttpContext.Session.GetString("Role").Equals("Admin"))
                    {
                        return Redirect("./ADMIN/Homepage");
                    }
                    if (HttpContext.Session.GetString("Role").Equals("Customer"))
                    {
                        return Redirect("./USER/Information");
                    }
                }
                return Page();
        }
        public async Task<IActionResult> OnPost() {
            if (Account == null)
            {
                ViewData["Nofication"] = "Please login";
                return Page();
            }
            
            if (Account.Email == null || Account.Password == null)
            {
                ViewData["Nofication"] = "Wrong account";
                return Page();
            }
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
            string emailAdmin = config["AdminAccount:email"];
            string passwordAdmin = config["AdminAccount:password"];
            if (emailAdmin.Equals(Account.Email) && passwordAdmin.Equals(Account.Password))
            {
                HttpContext.Session.SetString("Role", "Admin");
               return RedirectToPage("/ADMIN/Homepage");
            }
            IList<Customer> customers=(IList<Customer>)_customerRepository.GetAll();
            foreach (Customer customer in customers)
            {
                if (Account.Email.Equals(customer.Email))
                {
                    if (Account.Password.Equals(customer.Password))
                    {
                        HttpContext.Session.SetString("Role", "Customer");
                        HttpContext.Session.SetString("ID", customer.CustomerId.ToString());
                        return RedirectToPage("/USER/Information");
                    }
                    else
                    {
                        ViewData["Nofication"] = "Wrong password";
                        return Page();
                    }
                }
    
            }
            ViewData["Nofication"] = "Wrong account";
            return Page();
        }
    }
}

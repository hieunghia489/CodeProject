using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.CustomerRepository;
using DataAccess.OrderRepository;

namespace WebApplication.Pages.OrderFlower
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerRepository customerRepository;
        public DeleteModel(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
        }

        [BindProperty]
      public Order Order { get; set; } = default!;

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
            if (id == null || orderRepository.GetAll == null)
            {
                return Redirect("./Index");
            }
            Order = orderRepository.GetById(id);
            Order.Customer = customerRepository.GetById(Order.CustomerId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null || orderRepository.GetAll == null)
            {
                return Redirect("/");
            }

            if (orderRepository.GetById(id) == null)
            {
                return Redirect("./Index");
            }
            orderRepository.DeleteById(id);

            return RedirectToPage("./Index");
        }
    }
}

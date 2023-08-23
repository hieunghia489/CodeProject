using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.OrderRepository;
using DataAccess.CustomerRepository;

namespace WebApplication.Pages.OrderFlower
{
    public class EditModel : PageModel
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerRepository customerRepository;
        public EditModel(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;
        [BindProperty]
        public IEnumerable<OrderDetail> details { get; set; }

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
            if (id == null || orderRepository.GetById(id) == null)
            {
                return Redirect("./Index");
            }
            Order = orderRepository.GetById(id);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(Order.ShippedDate!=null)
            {
 if (DateTime.Compare(Order.OrderDate,(DateTime)Order.ShippedDate) != -1)
            {
                    ViewData["CustomerId"] = new SelectList(customerRepository.GetAll(), "CustomerId", "CustomerId");
                    ViewData["Nofication"] = "Shipped Date can't be sooner than Order Date";
                return Page();
            }
            }
            if (Order.Total < 0)
            {
                ViewData["CustomerId"] = new SelectList(customerRepository.GetAll(), "CustomerId", "CustomerId");
                ViewData["Nofication"] = "Total money must be POSITIVE";
                return Page();
            }
            orderRepository.Update(Order);
            return RedirectToPage("./Index");
        }
    }
}

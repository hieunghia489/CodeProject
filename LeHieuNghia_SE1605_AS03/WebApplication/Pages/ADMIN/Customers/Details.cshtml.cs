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

namespace WebApplication.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderRepository orderRepository;
       public DetailsModel (ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            this.customerRepository = customerRepository;
            this.orderRepository = orderRepository;
        }

      public Customer Customer { get; set; } = default!;
        public List<Order> customerOrders { get; set; }=new List<Order>();
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
            if (id == null || customerRepository.GetById(id)==null)
            {
                return RedirectToPage("/index");
            }
                Customer = customerRepository.GetById(id);
            IEnumerable<Order> allOrder = orderRepository.GetAll();
            foreach (Order o in allOrder)
            {
                if (o.CustomerId == id)
                {
                    customerOrders.Add(o);
                }
            }
            return Page();
        }
    }
}

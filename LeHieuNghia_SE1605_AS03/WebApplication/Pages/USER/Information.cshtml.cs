using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
/*using Microsoft.EntityFrameworkCore;*/
using BusinessObject.Models;
using DataAccess.CustomerRepository;
using DataAccess.OrderDetailRepository;
using DataAccess.OrderRepository;

namespace WebApplication.Pages.USER
{
    public class InformationModel : PageModel
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderDetailRepository orderDetailRepository;
        public InformationModel(ICustomerRepository customerRepository ,IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            this.customerRepository = customerRepository;
             this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
        }
        [BindProperty]
        public Customer Customer { get; set; } = default!;
        [BindProperty]
        public bool IsAdmin { get; set; }
        [BindProperty]
        public List<Order> customerOrders { get; set; } = new List<Order>();
        public async Task<IActionResult> OnGetAsync()
        {

            if (HttpContext.Session.GetString("Role") == null)
            {
                return RedirectToPage("/Welcome");
            }

            if (HttpContext.Session.GetString("Role").Equals("Customer"))
            {
                int id = int.Parse(HttpContext.Session.GetString("ID"));
                Customer=customerRepository.GetById(id);
                IsAdmin = false;
                IEnumerable<Order> allOrder = orderRepository.GetAll();
                foreach (Order o in allOrder)
                {
                    if (o.CustomerId == id)
                    {
                        customerOrders.Add(o);
                    }
                }
            }
            else
            {
                IsAdmin= true;
            }

             return Page();
        }
        public IActionResult OnPost()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Welcome");
        }
    }
}

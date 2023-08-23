using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.OrderRepository;
using DataAccess.CustomerRepository;
using DataAccess.OrderDetailRepository;

namespace WebApplication.Pages.OrderFlower
{
    public class DetailsModel : PageModel
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderDetailRepository detailRepository;
        public DetailsModel(IOrderRepository orderRepository, ICustomerRepository customerRepository, IOrderDetailRepository detailRepository)
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
            this.detailRepository = detailRepository;   
        }

        public Order Order { get; set; } = default!;
        public IEnumerable<OrderDetail> Details { get; set; } = default;

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
            Order.Customer=customerRepository.GetById(Order.CustomerId);
            if (detailRepository.GetByOrderId(id) != null)
            {
                Details=detailRepository.GetByOrderId(id);
            }
            return Page();
        }
    }
}

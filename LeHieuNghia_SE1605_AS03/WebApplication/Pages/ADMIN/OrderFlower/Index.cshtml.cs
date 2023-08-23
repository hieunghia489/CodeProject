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

namespace WebApplication.Pages.OrderFlower
{
    public class IndexModel : PageModel
    {
        private readonly IOrderRepository orderRepository;
        public IndexModel(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                return RedirectToPage("/Welcome");
            }
            if (!HttpContext.Session.GetString("Role").Equals("Admin"))
            {
                return RedirectToPage("/Unauthen");
            }
           

            Order = (IList <Order>) orderRepository.GetAll(); 
            return Page();
        }
    }
}

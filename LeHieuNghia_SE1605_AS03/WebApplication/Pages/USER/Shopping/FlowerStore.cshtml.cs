using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.FlowerBouquetRepository;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using BusinessObject.DataTransferObject;
using Microsoft.AspNetCore.Components.Forms;
using System.Configuration;
using Microsoft.CodeAnalysis;
using System.Globalization;
using DataAccess.OrderDetailRepository;
using DataAccess.OrderRepository;

namespace WebApplication.Pages.USER.Shopping
{
    public class FlowerStoreModel : PageModel
    {

        private readonly IFlowerBouquetRepository flowerBouquetRepository;
        public FlowerStoreModel(IFlowerBouquetRepository flowerBouquetRepository)
        {
            this.flowerBouquetRepository = flowerBouquetRepository;
        }

        [BindProperty]
        public IList<FlowerBouquet> FlowerBouquet { get; set; } = default!;
        [BindProperty]
        public CartDTO CartDTO { get; set; }
        [BindProperty]
        public bool isAdmin { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                return RedirectToPage("/Welcome");
            }
            if (HttpContext.Session.GetString("Role").Equals("Customer"))
            {
                isAdmin = false;
                FlowerBouquet = (IList<FlowerBouquet>)flowerBouquetRepository.GetAll();
            }
            else
            {
                isAdmin = true;
            }
           
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            CartDTO cart = new CartDTO();
            IList<FlowerBouquet> list =(IList<FlowerBouquet>) flowerBouquetRepository.GetAll();
            string[] idFlower = Request.Form["flowerID"];
            string[] quantity = Request.Form["quantity"];
            for (int i = 0; i< list.Count; i++)
            {
                int x = int.Parse(quantity[i]);
                int id = int.Parse(idFlower[i]);
                if (x > 0)
                {
                    FlowerBouquet dto = flowerBouquetRepository.GetById(id);
                    if (x <= dto.UnitsInStock)
                    {
                cart.AddItem(dto, x);
                        dto.UnitsInStock -= x;
                        flowerBouquetRepository.Update(dto);
                    }
                    else
                    {
                        ViewData["Nofication"] = "There are some flowers that not enough in stock to sell for you";
                        FlowerBouquet = (IList<FlowerBouquet>)flowerBouquetRepository.GetAll();
                        return Page();
                    }

                }
                else if(x<0)
                {
                    ViewData["Nofication"] = "Please check your Flower quantity";
                    FlowerBouquet = (IList<FlowerBouquet>)flowerBouquetRepository.GetAll();
                    return Page();
                }
            }
            if (cart.GetList().Count != 0)
            {OrderDetailRepository repoDetail= new OrderDetailRepository();
                Order order=new Order();
                int orderID = Random.Shared.Next(0, 100);
                order.OrderId = orderID;
                int total=0;
                order.CustomerId = int.Parse(HttpContext.Session.GetString("ID"));
                order.OrderDate = DateTime.Now;
                order.OrderStatus = "Just order";
                OrderRepository repoOrder = new OrderRepository();
                repoOrder.Save(order);
                foreach (CartItem item in cart.GetList())
                {
                    OrderDetail detail = new OrderDetail();
                    detail.OrderId = orderID;
                    detail.FlowerBouquetId = item.flower.FlowerBouquetId;
                    detail.Quantity = item.quantity;
                    detail.Discount = 0;
                    total += (int)item.flower.UnitPrice*item.quantity; 
                    repoDetail.SaveOrderDetail(detail);
                }
                order.Total=total;
                repoOrder.Update(order);
            }
            return Redirect("/USER/Information");
               
        }

    }
}

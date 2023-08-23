using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.FlowerBouquetRepository;
using Microsoft.AspNetCore.SignalR;
using WebApplication.Hubs;

namespace WebApplication.Pages.Flower
{
    public class IndexModel : PageModel
    {
        private readonly IFlowerBouquetRepository _flower;
        private readonly IHubContext<UploadList> _hubContext;
        public IndexModel(IFlowerBouquetRepository flower,IHubContext<UploadList> hubContext)
        {
            this._flower = flower;
            _hubContext = hubContext;
        }
        public IList<FlowerBouquet> FlowerBouquet { get;set; } = default!;

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
            
            FlowerBouquet =(IList<FlowerBouquet>)_flower.GetAll();
            return Page();
        }
        public async void UpdateListFlower(IList<FlowerBouquet> list)
        {
            await _hubContext.Clients.All.SendAsync("UpdateListFlower", list);
        }
    }
}

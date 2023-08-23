using BusinessObject.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
namespace WebApplication.Hubs
{
    public class UploadList:Hub
    {
        public async Task UpdateFlowerList(IList<FlowerBouquet> list)
        {
            await Clients.All.SendAsync("UpdateListFlower", list);
        }
    }
}

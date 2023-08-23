using BusinessObject.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DataTransferObject
{
    public class CartItem
    {
        public int quantity { get; set; }
        public FlowerBouquet flower { get; set; }        
        public CartItem() { 
        
        }
        public CartItem( int quantity, FlowerBouquet flower)
        {
            this.quantity = quantity;
            this.flower = flower;
        }
    }
    public class CartDTO
    {
        List<CartItem> list { get; set; }
        public CartDTO() { 
            this.list = new List<CartItem>();
        }
        
        public void AddItem(FlowerBouquet addFlower,int quantity)
        {
            if (this.list == null||this.list.Count==0)
            {
                this.list = new List<CartItem>();
            }
            foreach(CartItem item in this.list)
            {
                if (item.flower.FlowerBouquetId == addFlower.FlowerBouquetId)
                {
                    item.quantity += quantity;
                    break;
                }
            }
            list.Add(new CartItem(quantity, addFlower));
        }
        public void RemoveItem(FlowerBouquet delFlower)
        {
            if (this.list != null)
            {
               foreach(CartItem item in this.list)
                {
                    if (item.flower.FlowerBouquetId == delFlower.FlowerBouquetId)
                    {
                        this.list.Remove(item);
                        break;
                    }
                }
            }
        }
        public List<CartItem> GetList() { return this.list; }
        public void SetList(List<CartItem> list) { this.list = list; }
        public CartItem GetCartItem(int id) {
            foreach (CartItem item in this.list)
            {
                if (item.flower.FlowerBouquetId == id) return item;
            }
            return null;
        }
        public void DeleteItem(FlowerBouquet delFlower)
        {
            if (!this.list.IsNullOrEmpty())
            {
                foreach(CartItem item in this.list)
                {
                    if (item.flower.FlowerBouquetId == delFlower.FlowerBouquetId)
                    {
                        item.quantity -= 1;
                        if (item.quantity <= 0) RemoveItem(delFlower); break;
                    }
                }
            }
        }
    }
}

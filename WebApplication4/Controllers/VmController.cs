using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.App_Code;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [ApiController]
    public class VmController : ControllerBase
    {
        ApplicationContext db;
        

        public VmController(ApplicationContext context)
        {
            db = context;
            
        }


        //возвращаем данные
        [HttpGet("/api/getdata")]
        public IActionResult GetData()
        {
            return Ok(new
            {
                vmWallet = db.Wallets.Where(x => x.User.Name == "VM" && x.Quantity > 0),
                customerWallet = db.Wallets.Where(x => x.User.Name == "CUSTOMER" && x.Quantity > 0),
                currentMoney = db.Wallets.Where(x => x.User.Name == "TEMP")
                .ToList()
                .GetSum()
            });


        }

        [HttpGet("/api/products")]
        public IEnumerable<Product> GetProducts()
        {
            return db.Products.Where(x=>x.Quantity>0).ToList();
        }

        [HttpPost("/api/addcoin")]
        public IActionResult AddCoin([FromBody]Coins coin)
        {
            var sameCoin = db.Wallets.FirstOrDefault(x => x.User.Name == "TEMP" && x.Value == coin.Value);
            sameCoin.Quantity++;

            var sameCoinVM = db.Wallets.FirstOrDefault(x => x.User.Name == "VM" && x.Value == coin.Value);
            sameCoinVM.Quantity++;

            var samwCoinUser = db.Wallets.FirstOrDefault(x => x.User.Name == "CUSTOMER" && x.Value == coin.Value);
            samwCoinUser.Quantity--;

            db.SaveChanges();

            return GetData();
        }

        [HttpPost("api/return/{sum}")]
        public IActionResult ReturnMoney(int sum)
        {
            var tempWallet = this.tempWallet();         

            var vmWallet = db.Wallets.Where(x => x.User.Name == "VM" && x.Quantity > 0)             
                .OrderByDescending(x => x.Value)
                .ToList();       

            var customerWallet = db.Wallets.Where(x => x.User.Name == "CUSTOMER")
               .ToList();

            foreach (var item in vmWallet)
            {
                var coins = sum / item.Value;
                //можно отдать данной монетой
                if (coins > 0 )
                {
                    //монет хватает
                    if (coins<= item.Quantity)
                    {
                        sum -= item.Value * coins;
                        item.Quantity -= coins;
                        customerWallet.First(x => x.Value == item.Value).Quantity += coins;
                    }
                    //монет не хватает
                    else
                    {
                        sum -= item.Value * item.Quantity;
                        item.Quantity = 0;
                        customerWallet.First(x => x.Value == item.Value).Quantity += item.Quantity;
                    }
                }
                if (sum == 0)
                    break;
            }

            //все успешно
            if (sum == 0)
            {
                //убираем из временного
                tempWallet.ForEach(x => x.Quantity = 0);
                db.SaveChanges();                
            }
            //нет монет для сдачи
            else
            {
                return StatusCode(500);
            }


            return GetData();
        }

        [HttpPost("/api/buy/{money}")]
        public IActionResult Buy(int money, [FromBody]Product product)
        {
            var tempWallet = this.tempWallet();
            var sum = money - product.Price;

            db.Products.First(x => x.Id == product.Id).Quantity--;

            return ReturnMoney(sum);
        }

        private List<Coins> tempWallet()
        {
            return db.Wallets.Where(x => x.User.Name == "TEMP")
               .ToList();
        }

    

    }
}
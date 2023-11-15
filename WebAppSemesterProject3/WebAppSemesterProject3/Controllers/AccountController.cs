﻿using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAppSemesterProject3.DTO;
using WebAppSemesterProject3.Models;

namespace WebAppSemesterProject3.Controllers
{
    public class AccountController : Controller
    {
        

        public async Task<IActionResult> Index()
        {
            Wallet wallet = new Wallet();
            List<Account> accounts = new List<Account> { new Account("Rasmus", "emailExample", 0d, 1, wallet) }; 
        
            using (Deserializer<Account> ad = new())
            {
                try
                {
                    //IEnumerable<Account> accounts = await ad.GetList();
                    return View(accounts);
                }
                catch (Exception ex)
                {
                    return View(accounts);
                }
            }
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Account account)
        {
            return View(account);
        }

    }
}

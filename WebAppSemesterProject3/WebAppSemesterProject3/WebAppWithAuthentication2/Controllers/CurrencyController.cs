﻿using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAppSemesterProject.Controllers
{
    public class CurrencyController : Controller
    {
        public async Task<IActionResult> Index()
        {
                try
                {
                   // IEnumerable<Currency> Currencies = await ad.GetList();
                    return View();
                }
                catch (Exception ex)
                {
                    return View();
                }
            }
        }
    }

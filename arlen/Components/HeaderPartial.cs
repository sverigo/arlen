﻿using arlen.Infrastructure;
using arlen.Models;
using Microsoft.AspNetCore.Mvc;

namespace arlen.Components
{
    public class HeaderPartial : ViewComponent
    {
        User account;
        
        public HeaderPartial(ArlenContext db)
        {
            AccountManager accountManager = new AccountManager(db);
            account = accountManager.GetAccount(1);
        }
        
        public IViewComponentResult Invoke()
        {
            return View(account);
        }
    }
}

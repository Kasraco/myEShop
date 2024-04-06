﻿using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myEShop.Web.Models;

namespace myEShop.Web.Components;

public class ProductInBasketComponent : ViewComponent
{
    private readonly myEShopContext _context;

    public ProductInBasketComponent(myEShopContext shopContext)
    {
        _context = shopContext;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        int UserId = 0;
        int CountOrder = 0;
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == User!.Identity!.Name);
        if (user != null)
            UserId = user.UserId;
        var order = await _context.Orders
                            .Where(x => x.UserId == UserId && x.IsFinaly == false)
                            .Include(x => x.OrderDetails)
                            .FirstOrDefaultAsync();
        if (order != null)
            CountOrder = order.OrderDetails.Sum(x => x.Count);

        return View("Components/ProductInBasketComponent.cshtml", CountOrder);
    }
}


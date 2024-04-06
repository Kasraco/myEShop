using Microsoft.AspNetCore.Mvc;
using myEShop.Web.Models.ViewModels;

namespace myEShop.Web.Components;

public class ProductGroupsComponent : ViewComponent
{
    private readonly myEShopContext _context;

    public ProductGroupsComponent(myEShopContext shopContext)
    {
        _context = shopContext;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var _cats = _context.Categories
                          .Select(x => new ShowGroupViewModel
                          {
                              GroupId = x.Id,
                              GroupName = x.Name,
                              ProductCount = x.categoryToProducts.Count(c => c.CategoryId == x.Id)
                          }).ToList();
        return View("Components/ProductGroupsComponent.cshtml", _cats);
    }
}

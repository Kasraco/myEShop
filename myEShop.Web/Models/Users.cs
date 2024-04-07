using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace myEShop.Web.Models;

public class User : IdentityUser<int>
{

    public DateTime RegisterDate { get; set; }
    public List<Order> orders { get; set; }
}

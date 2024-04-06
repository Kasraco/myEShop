using System.ComponentModel.DataAnnotations.Schema;
using myEShop.Web.Models;

namespace myEShop.Web;

public class CategoryToProduct
{

   
    public int CategoryId { get; set; }
    public int ProductId { get; set; }

    //Navigation Property
    public Category Category { get; set; }
    public Product Product { get; set; }
}

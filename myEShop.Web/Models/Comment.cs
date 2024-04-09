using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using myEShop.Web.Models;

namespace myEShop.Web;

public class Comment
{
    [Key]
    public int CommentId { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string CommentName { get; set; }

    [Column(TypeName = "ntext")]
    [Required]
    public string CommentText { get; set; }

    [Required]
    public int ProductId { get; set; }

    public bool IsPublished { get; set; }

}

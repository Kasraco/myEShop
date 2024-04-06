﻿using System.ComponentModel.DataAnnotations;

namespace myEShop.Web.Models;

public class User
{

    [Key]
    public int UserId { get; set; }

    [Required]
    [MaxLength(300)]

    public string Email { get; set; }

    [Required]
    [MaxLength(50)]
    public string Password { get; set; }

    [Required]
    public DateTime RegisterDate { get; set; }

    public bool IsAdmin { get; set; }


    public List<Order> orders { get; set; }
}

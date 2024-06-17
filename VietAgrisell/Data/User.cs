using System;
using System.Collections.Generic;

namespace VietAgrisell.Data;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? UserName { get; set; }

    public string Mobile { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public DateTime DateOfBirth { get; set; }

    public int Role { get; set; }

    public string? RandomKey { get; set; }

    public bool Available { get; set; }

    public bool Gender { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
}

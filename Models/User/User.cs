using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KyrsachAPI.Models.User;

public partial class User
{
    [Key]
    public int UserId { get; set; }

    public string UserLogin { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string UserFirstName { get; set; } = null!;

    public string UserLastName { get; set; } = null!;

    public string? UserMiddleName { get; set; }

    public DateTime UserCreateDate { get; set; }

    public string UserEmail { get; set; } = null!;

    public bool UserActiveStatus { get; set; }

    public int UserRoleId { get; set; }
}

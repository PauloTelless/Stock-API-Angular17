﻿using Microsoft.AspNetCore.Identity;

namespace ApiCategoriaProdutoAngular.Models;

public class AplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }

    public DateTime RefreshTokenExpiryTime { get; set; }    
}

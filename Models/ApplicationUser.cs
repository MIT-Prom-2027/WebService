using Microsoft.AspNetCore.Identity;

namespace OutilAdmin.Models;
public class ApplicationUser : IdentityUser
{
    public int Age { get; set; }
}
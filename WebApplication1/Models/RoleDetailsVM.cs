using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using WebApplication1.DomainLayer.Entities.Concrete;

namespace WebApplication1.PresentationLayer.Models
{
    public class RoleDetailsVM
    {
        public IdentityRole Role { get; set; }
        public List<User> Members { get; set; }
        public List<User> NonMembers { get; set; }
    }
}

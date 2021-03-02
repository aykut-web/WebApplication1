using System;

namespace WebApplication1.PresentationLayer.Models
{
    public class UserVm
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

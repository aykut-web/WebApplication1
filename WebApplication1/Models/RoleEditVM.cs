namespace WebApplication1.PresentationLayer.Models
{
    public class RoleEditVM
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}

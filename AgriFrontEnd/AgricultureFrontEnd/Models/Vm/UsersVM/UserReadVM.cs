    namespace AgricultureFrontEnd.Models.Vm.UsersVM;

    public class UserReadVM
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
    }

    public enum Roles
    {
        Admin,
        User,
        Viewer
    }
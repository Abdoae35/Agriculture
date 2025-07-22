using System.ComponentModel.DataAnnotations;
using AgricultureFrontEnd.Models.Vm.UsersVM;

namespace AgricultureFrontEnd.Models;

public class RegisterVM
{
   
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Role is required.")]
    public Roles? Role { get; set; }   
}
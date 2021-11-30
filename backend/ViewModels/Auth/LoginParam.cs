using System.ComponentModel.DataAnnotations;
using backend.Models;

namespace backend.ViewModels
{

  public class LoginParam
  {
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
  }
}

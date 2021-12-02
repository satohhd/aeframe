using System.ComponentModel.DataAnnotations;
using backend.Models;

namespace backend.ViewModels.Common
{

  public class Pagination
  {
    public int Rows { get; set; } = 0;
    public int PerPage { get; set; } = 5;
    public int CurrentPage { get; set; } = 1;
  }
}

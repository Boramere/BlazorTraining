using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAppDemo.Services;

public class Superhero
{
    public int Id { get; set; }
    [Required, MinLength(5)]
    public string Name { get; set; }
    [Required, MinLength(5)]
    public string RealName { get; set; }
    [Required, MinLength(5)]
    public string Powers { get; set; }
}

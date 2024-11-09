using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahnaMahna.Shared.Models;

public class TodoItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Text { get; set; }

    [Required]
    public TodoItemState State { get; set; }

    public ICollection<Category> Categories { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace WebApplicationTestProject1.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name cant be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "ImageUrl cant be empty")]

        public string ImageUrl { get; set; }

        public ICollection<Property> Properties { get; set; }
    }
}

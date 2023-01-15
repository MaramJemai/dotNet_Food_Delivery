using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class Plate
    {
        public int Id { get; set; } 
        public string? Name { get; set; }    

        public double Price { get; set; }
        [DataType(DataType.MultilineText)]

        public string? Description { get; set; }
        [DataType(DataType.ImageUrl)]

        public string? Photo { get; set; }
        public virtual ICollection<Meal>? Meals { get; set; }

    }
}

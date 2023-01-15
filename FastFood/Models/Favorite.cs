using FastFood.Areas.Identity.Data;

namespace FastFood.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public virtual FastFoodUser? Client { get; set; }

        public virtual Plate? Plate { get; set; }
    }
}

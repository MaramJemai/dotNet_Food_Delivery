using System.ComponentModel.DataAnnotations;
using FastFood.Areas.Identity.Data;

namespace FastFood.Models
{
    public class Order
    {
        public Order()
        {
            Meals = new List<Meal>();
        }
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } 
        [DataType(DataType.Url)]
        public double Total { get; set; } 
        public string? PayementMode { get; set; } = null!;
        public EnumOrderState State { get; set; } 
        public virtual FastFoodUser? Client { get; set; }
        public virtual ICollection<Meal>? Meals { get; set; }


    }
    public enum EnumOrderState
    {
        [Display(Name = "Waiting for validation")]
        Waiting,
        [Display(Name = "Order was completed")]
        Completed
    }
}

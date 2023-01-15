namespace FastFood.Models
{
    public class Meal
    {
        public int Id { get; set; } 
        public int Number { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Plate? Plate { get; set; }


    }
}

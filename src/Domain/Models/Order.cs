using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required] public string? FullName { get; set; }
        [Required] public string? ContactNumber { get; set; }
        [Required] public string? KTP { get; set; }

        public DateOnly DateDepart { get; set; } = new DateOnly(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day
            );
        public DateOnly DateReturn { get; set; } = new DateOnly(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day
            );

        [NotMapped] private int rentDuration;
        public int RentDuration
        {
            get {
                rentDuration = DateReturn.DayOfYear - DateDepart.DayOfYear;
                return rentDuration;
            }
            init {
                rentDuration = value;
            }
        }
        public bool WithDriver { get; set; }
        [NotMapped] private int cost;
        public int Cost
        {
            get {
                cost = 250000 * RentDuration;
                return cost;
            }
            init {
                cost = value;
            }
        }
        public int RentStatus { get; set; }
        public Car? Car { get; set; }
    }
}
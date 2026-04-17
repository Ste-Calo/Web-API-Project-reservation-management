using Microsoft.EntityFrameworkCore;
namespace RestaurantSaaS.Models
{
//Defining the properties of the “reservations” entity with constraints for EF Core and foreign keys to the table
public class Reservation
    {
        public int Id {get;set;}
        public required string CustomerName {get; set;}
        public string? CustomerPhone {get; set;}
        public DateTime ReservationTime {get; set;}
        public int People {get; set;}
        public int TableId {get; set;}
    }
}
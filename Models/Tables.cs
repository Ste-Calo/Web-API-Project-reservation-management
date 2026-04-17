using Microsoft.EntityFrameworkCore;
namespace RestaurantSaaS.Models
{
//Defining the properties of the "Table" entity with implementation of the soft-delete pattern
public class Table
{
   public int Id {get; set;}
   public int Seat {get; set;}
   public bool IsActive {get; set;} = true;
}
}
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace soft20181_starter.Models
{
    [PrimaryKey("UserId", "EventId")]
    public class Booking
    {
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        [Required]
        [ForeignKey("Event")]
        public int EventId { get; set; } 

        public int quantity { get; set; }

        public Booking()
        {
        }
        public Booking (string userId, int eventId)
        {
            UserId = userId;
            EventId = eventId;
        }

        public Booking(string userId, int eventId, int quantity)
        {
            UserId = userId;
            EventId = eventId;
            this.quantity = quantity;
        }
    }
}

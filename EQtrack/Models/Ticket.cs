using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace EQtrack.Models
{

    //id, timestamp
    public class Ticket
    {
        //Both are required for security of the tool itself
        [Required]
        public int Id { get; set; }
        [Display(Name = "Time checked out"), Required]
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        //Things to consider. The checkout ticket has id for itself, but not the tool or person checking it out?
        //We may need to add those sooner or later.

    }
}

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




        //toolid with foreign key
        [ForeignKey("Tool"), Required]
        public int toolID { get; set; }
        [Display(Name = "Tools")]
        public tool? Tool { get; set; }


        //show count function

        public int getCount(){

            /*
            int toolId2 = toolID;
            int? count = 0;
            if (Tool.id != null) { 
             count = Tool.id;
            }

            if( count == null){ count = 0; }
            */
            int? count = 0;
            return (int)count;
        }


        //ticket model or controller is going to have to show what the count is, 

        //add varible for the person renting the ticket, as well as in the return ticket
        //user e-mail
        public string? userEmail { get; set; }


        //Return to... Duh

        //[ForeignKey("ReturnToInventory1")]
        public int? InventoryId1 { get; set; }
        //public inventory? ReturnToInventory1 { get; set; }
    }
}

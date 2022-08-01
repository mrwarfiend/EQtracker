using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EQtrack.Models
{
    //id, timestamp, toolId(foreignkey), condition, bool repair/goodtogo
    //if this model represents the return ticket, then said ticket will need to be done by reciver,
    //add autherizations to database and controller for this model as well, set levels to adimn or other
    //nessicary roles.
    public class ReturnTicket
    {
        
        //[Required]
        public int id { get; set; }
        [Display(Name = "Time of Return"), Required]
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        //consider placing this or a copy in the ticket model
        [ForeignKey("Tool"), Required]
        public int toolID;
        public tool? Tool { get; set; }

        //

        [Display(Name = "Condition")]
        public string Condition { get; set; }
        [Required]
        public bool repairNeeded;


    }
}

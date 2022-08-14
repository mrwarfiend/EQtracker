using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System;


namespace EQtrack.Models
{
    public class DamagedItem
    {

        public int id { get; set; }
        //
        [Display(Name = "Previous user"), Required]
        public string? userId { get; set; }

        //
        [Display(Name = "Tool Id #"),ForeignKey("Tools")]
        public int toolId { get; set; }
        public tool? Tools { get; set; }
        //
        //public int count { get; set; }
        //

        [Display(Name = "Time recived"), Required]
        public DateTime timeStamp { get; set; }
        //public bool check { get; set; }


        //
        [Display(Name = "Condition"), Required]
        public string? Condition { get; set; }

        //
        [Display(Name = "Repair needed?"),Required]
        public bool? repairNeeded { get; set; }


        [Display(Name = "Rentor inventory id"),ForeignKey("ReturnToInventory")]
        public int? InventoryId { get; set; }
        public inventory? ReturnToInventory { get; set; }


        //Inintally the AdminId, will be set as NONE
        //but upon returning, will be auto filled by system
        [Display(Name = "Admin id"), Required]
        public string? AdminId { get; set; }
        //Similar to above, but will be allowed to be blank
        [Display(Name = "Return time")]
        public DateTime timeStamp2 { get; set; }

        //so new to this model, admin id, timestamp2,
    }
}

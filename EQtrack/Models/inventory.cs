﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EQtrack.Models
{
    public class inventory
    {

        //id, name, toolId(foreignkey), count

        public int id { get; set; }
        

        [Display(Name = "Inventory"), Required]
        public string? name { get; set; }



        [ForeignKey("Tool"), Required]
        public int toolID { get; set; }
        [Display(Name = "Tools")]
        public tool? Tool { get; set; }

        // int.MaxValue
        //changed 8.11.2022
        [Range(0, 1000, ErrorMessage = "Please enter a value at least as big as 1 to 1000 for  {1}"), Required]
        public int Count { get; set; }
        //below isn't really used
        public bool flag { get; set; }

        //move flag form tools to inventory
        //might just keep flag in tools.
        //may require list of tools

    }
}

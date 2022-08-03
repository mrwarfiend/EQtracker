using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EQtrack.Models
{
    public class inventory
    {

        //id, name, toolId(foreignkey), count

        public int id { get; set; }

        [Display(Name = "Inventory Name?"), Required]
        public string? name { get; set; }



        [ForeignKey("Tool"), Required]
        public int toolID { get; set; }
        [Display(Name = "Tools")]
        public tool? Tool { get; set; }

        public int count { get; set; }
        public bool flag { get; set; }

        //move flag form tools to inventory
        //might just keep flag in tools.
        //may require list of tools

    }
}

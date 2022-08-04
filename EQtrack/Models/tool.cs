using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EQtrack.Models
{
    public class tool
    {
        //id, name, categId(foreignkey), flag, img, desc

        public int id { get; set; }

        [Display(Name = "Tool Name"), Required]
        public string? name { get; set; }





        [Display(Name = "Category Name"), ForeignKey("Categ"), Required]
        public int? categID { get; set; }
        public Category? Categ { get; set; }


        //this may go unused
        public bool? flag { get; set; }

        [Display(Name = "Description")]
        public string? desc { get; set; }

        public string? image { get; set; }

    }
}

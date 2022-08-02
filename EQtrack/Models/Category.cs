using System;
using System.ComponentModel.DataAnnotations;

namespace EQtrack.Models
{
    public class Category
    {
        //id: used by tools to identify catagory, etc
        public int id { get; set; }

        [Display(Name = "Category Name"), Required]

        public string? name { get; set; }


        [Display(Name = "Description")]
        public string? desc { get; set; }


    }
}

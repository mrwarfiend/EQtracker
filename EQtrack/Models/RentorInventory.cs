using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System;

namespace EQtrack.Models
{
    public class RentorInventory
    {
        public int id { get; set; }
        public string? userId { get; set; }
        //
        [ForeignKey("Tools")]
        public int toolId { get; set; }
        public tool? Tools { get; set; }

        public int count{ get; set; }
        //public float subtotal{ get; set; }
        //public float tax{ get; set; }
        //public float total{ get; set; }
        public DateTime timeStamp { get; set; }
        public bool check { get; set; }
        //
        //[ForeignKey("ReturnToInventory")]
        public int? InventoryId { get; set; }
        //public inventory? ReturnToInventory { get; set; }

    }
}

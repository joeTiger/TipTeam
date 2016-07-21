using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TipTeam.Models
{
    public class TipViewModel
    {
        public int Id { get; set; }
        // The ProductName property is required.
        [Required]
        public string Data { get; set; }
        [Required]
        public string Un { get; set; }
    }
}
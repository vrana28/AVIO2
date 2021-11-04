using Avio.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Avio.WebApp.Models
{
    public class CreateFlightViewModel
    {
        [Required]
        public Flight Flight { get; set; }
        [Required]
        public List<SelectListItem> Users { get; set; }
    }
}

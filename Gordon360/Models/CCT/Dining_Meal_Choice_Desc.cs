﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gordon360.Models.CCT
{
    [Keyless]
    public partial class Dining_Meal_Choice_Desc
    {
        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string Meal_Choice_Id { get; set; }
        [StringLength(60)]
        [Unicode(false)]
        public string Meal_Choice_Desc { get; set; }
    }
}
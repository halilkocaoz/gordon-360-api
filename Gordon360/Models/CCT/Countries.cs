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
    public partial class Countries
    {
        [Required]
        [StringLength(2)]
        [Unicode(false)]
        public string CTY { get; set; }
        [Required]
        [StringLength(63)]
        [Unicode(false)]
        public string COUNTRY { get; set; }
    }
}
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
    public partial class Majors
    {
        [StringLength(5)]
        [Unicode(false)]
        public string MajorCode { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MajorDescription { get; set; }
    }
}
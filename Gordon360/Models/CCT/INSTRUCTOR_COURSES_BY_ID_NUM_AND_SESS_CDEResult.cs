﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gordon360.Models.CCT
{
    public partial class INSTRUCTOR_COURSES_BY_ID_NUM_AND_SESS_CDEResult
    {
        public int? ID_NUM { get; set; }
        public string CRS_CDE { get; set; }
        public string CRS_TITLE { get; set; }
        public string BLDG_CDE { get; set; }
        public string ROOM_CDE { get; set; }
        public string MONDAY_CDE { get; set; }
        public string TUESDAY_CDE { get; set; }
        public string WEDNESDAY_CDE { get; set; }
        public string THURSDAY_CDE { get; set; }
        public string FRIDAY_CDE { get; set; }
        public TimeSpan? BEGIN_TIME { get; set; }
        public TimeSpan? END_TIME { get; set; }
    }
}

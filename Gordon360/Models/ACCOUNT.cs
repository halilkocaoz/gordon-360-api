//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gordon360.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ACCOUNT
    {
        public int account_id { get; set; }
        public string gordon_id { get; set; }
        public string barcode { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string AD_Username { get; set; }
        public string account_type { get; set; }
        public string office_hours { get; set; }
        public int Primary_Photo { get; set; }
        public int Preferred_Photo { get; set; }
        public int show_pic { get; set; }
        public int Private { get; set; }
        public int ReadOnly { get; set; }
        public int is_police { get; set; }
        public Nullable<int> Chapel_Required { get; set; }
        public string Mail_Location { get; set; }
        public Nullable<int> Chapel_Attended { get; set; }
    }
}

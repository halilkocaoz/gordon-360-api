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
    
    public partial class MEMBERSHIPS_PER_ACT_CDE_AUTHORIZED_Result
    {
        public int MembershipID { get; set; }
        public string ActivityCode { get; set; }
        public string ActivityDescription { get; set; }
        public string ActivityImagePath { get; set; }
        public string SessionCode { get; set; }
        public string SessionDescription { get; set; }
        public int IDNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Participation { get; set; }
        public string ParticipationDescription { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Description { get; set; }
        public Nullable<bool> GroupAdmin { get; set; }
    }
}

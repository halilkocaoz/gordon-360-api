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
    
    public partial class GET_SINGLE_MESSAGE_BY_ID_Result
    {
        public string message_id { get; set; }
        public string text { get; set; }
        public System.DateTime createdAt { get; set; }
        public string user_id { get; set; }
        public byte[] image { get; set; }
        public byte[] video { get; set; }
        public byte[] audio { get; set; }
        public bool system { get; set; }
        public bool sent { get; set; }
        public bool received { get; set; }
        public bool pending { get; set; }
    }
}

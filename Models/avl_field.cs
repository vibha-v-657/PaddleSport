//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hackathon_Internship.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class avl_field
    {
        public int field_id { get; set; }
        public int court_id { get; set; }
    
        public virtual availability availability { get; set; }
        public virtual court court { get; set; }
    }
}

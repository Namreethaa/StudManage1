//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudManage1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class result
    {
        public int sid { get; set; }
        public Nullable<int> id { get; set; }
        public Nullable<int> m1 { get; set; }
        public Nullable<int> m2 { get; set; }
        public Nullable<int> m3 { get; set; }
        public Nullable<int> total { get; set; }
        public string grade { get; set; }
    
        public virtual stuDetail stuDetail { get; set; }
    }
}

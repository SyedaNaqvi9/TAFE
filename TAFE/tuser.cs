//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TAFE
{
    using System;
    using System.Collections.Generic;
    
    public partial class tuser
    {
        public int userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string userType { get; set; }
        public Nullable<int> teacherId { get; set; }
    
        public virtual teacher teacher { get; set; }
    }
}
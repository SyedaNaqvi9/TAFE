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
    
    public partial class enrollment
    {
        public int enrollmentId { get; set; }
        public string coursename { get; set; }
        public string semestername { get; set; }
        public string locationname { get; set; }
        public string mode { get; set; }
        public string status { get; set; }
        public string fee { get; set; }
        public string result { get; set; }
        public Nullable<int> studentId { get; set; }
        public Nullable<int> courseId { get; set; }
        public Nullable<int> locationId { get; set; }
    
        public virtual course course { get; set; }
        public virtual courselocation courselocation { get; set; }
        public virtual student student { get; set; }
    }
}

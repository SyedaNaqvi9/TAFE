﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class tafesystemEntities : DbContext
    {
        public tafesystemEntities()
            : base("name=tafesystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<student> students { get; set; }
        public virtual DbSet<teacher> teachers { get; set; }
        public virtual DbSet<suser> susers { get; set; }
        public virtual DbSet<course> courses { get; set; }
        public virtual DbSet<courseclusterunit> courseclusterunits { get; set; }
        public virtual DbSet<unit> units { get; set; }
        public virtual DbSet<tuser> tusers { get; set; }
        public virtual DbSet<teachercourse> teachercourses { get; set; }
        public virtual DbSet<admin> admins { get; set; }
        public virtual DbSet<auser> ausers { get; set; }
        public virtual DbSet<fee> fees { get; set; }
        public virtual DbSet<coursetimetable> coursetimetables { get; set; }
        public virtual DbSet<courselocation> courselocations { get; set; }
        public virtual DbSet<Inlocationcourse> Inlocationcourses { get; set; }
        public virtual DbSet<coursesemester> coursesemesters { get; set; }
        public virtual DbSet<Incoursesemester> Incoursesemesters { get; set; }
        public virtual DbSet<cluster> clusters { get; set; }
        public virtual DbSet<clusterlocation> clusterlocations { get; set; }
        public virtual DbSet<enrollment> enrollments { get; set; }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarbonFootPrint.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class footprintdbEntities : DbContext
    {
        public footprintdbEntities()
            : base("name=footprintdbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Apparel> Apparels { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<food_apparels_cloth_recycle_tips> food_apparels_cloth_recycle_tips { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Recycle> Recycles { get; set; }
        public virtual DbSet<Subscriber> Subscribers { get; set; }
        public virtual DbSet<Tip> Tips { get; set; }
    }
}

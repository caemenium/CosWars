﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CosWars.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DB_CosWarsEntities : DbContext
    {
        public DB_CosWarsEntities()
            : base("name=DB_CosWarsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cinsiyet> Cinsiyet { get; set; }
        public virtual DbSet<Cosplay> Cosplay { get; set; }
        public virtual DbSet<Etiket> Etiket { get; set; }
        public virtual DbSet<GenelBilgiler> GenelBilgiler { get; set; }
        public virtual DbSet<Kategori> Kategori { get; set; }
    }
}
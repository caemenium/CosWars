//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Cosplay
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cosplay()
        {
            this.Etiket = new HashSet<Etiket>();
        }
    
        public int ID { get; set; }
        public Nullable<System.DateTime> EklenmeTarihi { get; set; }
        public bool AktifMi { get; set; }
        public Nullable<int> Sira { get; set; }
        public string Adi { get; set; }
        public string ResimYolu { get; set; }
        public Nullable<int> KategoriID { get; set; }
        public Nullable<int> Vote { get; set; }
    
        public virtual Kategori Kategori { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Etiket> Etiket { get; set; }
    }
}
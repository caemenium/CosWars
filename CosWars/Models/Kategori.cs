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
    
    public partial class Kategori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kategori()
        {
            this.Cosplay = new HashSet<Cosplay>();
        }
    
        public int ID { get; set; }
        public Nullable<System.DateTime> EklenmeTarihi { get; set; }
        public bool AktifMi { get; set; }
        public Nullable<int> Sira { get; set; }
        public string Adi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cosplay> Cosplay { get; set; }
    }
}

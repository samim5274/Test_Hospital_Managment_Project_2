//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Login
{
    using System;
    using System.Collections.Generic;
    
    public partial class MarketingInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MarketingInfo()
        {
            this.ReferInfoes = new HashSet<ReferInfo>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> GenderId { get; set; }
        public string Address { get; set; }
        public Nullable<int> Phone { get; set; }
        public string Area { get; set; }
    
        public virtual GenderInfo GenderInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReferInfo> ReferInfoes { get; set; }
    }
}

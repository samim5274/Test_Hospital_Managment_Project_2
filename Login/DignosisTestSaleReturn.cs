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
    
    public partial class DignosisTestSaleReturn
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> ReturnDate { get; set; }
        public Nullable<System.DateTime> SaleDate { get; set; }
        public Nullable<int> DignosticId { get; set; }
        public Nullable<int> T_Reg { get; set; }
        public string TestReturnOfficer { get; set; }
        public Nullable<int> TotalCost { get; set; }
        public Nullable<int> VAT { get; set; }
        public Nullable<int> Discount { get; set; }
        public Nullable<int> Due { get; set; }
        public Nullable<int> Paid { get; set; }
        public Nullable<int> ReturnAmount { get; set; }
        public string Discription { get; set; }
    
        public virtual DignosisTestSaleInfo DignosisTestSaleInfo { get; set; }
    }
}
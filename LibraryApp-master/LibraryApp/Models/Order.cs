//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int Id { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public bool IsFinish { get; set; }
        public int DelayPrice { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public Nullable<int> AdminId { get; set; }
    
        public virtual Admin Admin { get; set; }
        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyThirdProject2
{
    using System;
    using System.Collections.Generic;
    
    public partial class User_Information
    {
        public string Country { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public short Year { get; set; }
        public short Month { get; set; }
        public short Day { get; set; }
        public int Id { get; set; }
    
        public virtual Player Player { get; set; }
    }
}

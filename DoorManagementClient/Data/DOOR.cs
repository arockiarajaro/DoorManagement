//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoorManagementClient.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class DOOR
    {
        public int DoorId { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Opened { get; set; }
        public Nullable<bool> Locked { get; set; }
        public Nullable<System.DateTime> Crt_dt { get; set; }
    }
}

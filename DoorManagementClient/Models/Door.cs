using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace DoorManagementClient.Model
{
    public class Door:BindableBase
    {
        
        public int DoorId { get; set; }
        public string Description { get; set; }
        private Nullable<bool> _Opened;
        public Nullable<bool> Opened
        {
            get { return _Opened; }
            set { SetProperty(ref _Opened, value); }
        }
        private Nullable<bool> _Locked;
        public Nullable<bool> Locked
        {
            get { return _Locked; }
            set { SetProperty(ref _Locked, value); }
        }
        public Nullable<System.DateTime> Crt_dt { get; set; }
    }
}

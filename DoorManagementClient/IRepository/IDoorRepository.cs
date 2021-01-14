using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorManagementClient.Data;

namespace DoorManagementClient.IRepository
{
    interface IDoorRepository : IDisposable
    {
        IEnumerable<DOOR> GetAllDoor();
        DOOR GetDoorById(int doorId);
        int AddDoor(DOOR doorEntity);
        int UpdateDoor(DOOR doorEntity);
        void DeleteDoor(int doorId);
    }
}

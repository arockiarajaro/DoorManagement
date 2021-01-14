using DoorManagementClient.IRepository;
using DoorManagementClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorManagementClient.Data;

namespace DoorManagementClient.Repository
{
    public class DoorRepository : IDoorRepository
    {
        private readonly DoorManagementContext doorManagementContext;
        public DoorRepository(DoorManagementContext context)
        {
            doorManagementContext= context;
        }
        public int AddDoor(DOOR doorEntity)
        {
            doorManagementContext.DOORs.Add(doorEntity);
            doorManagementContext.SaveChanges();
            return doorEntity.DoorId;
        }

        public void DeleteDoor(int doorId)
        {
            DOOR doorEntity = doorManagementContext.DOORs.Find(doorId);
            doorManagementContext.DOORs.Remove(doorEntity);
            doorManagementContext.SaveChanges();
        }
        public IEnumerable<DOOR> GetAllDoor()
        {
            return doorManagementContext.DOORs;
        }

        public DOOR GetDoorById(int doorId)
        {
            return doorManagementContext.DOORs.Find(doorId);
        }

        public int UpdateDoor(DOOR doorEntity)
        {
            doorManagementContext.Entry(doorEntity).State = System.Data.EntityState.Modified;
            doorManagementContext.SaveChanges();
            return doorEntity.DoorId;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    doorManagementContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}

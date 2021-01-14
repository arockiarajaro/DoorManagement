using System;
using System.Linq;
using System.Windows.Input;
using Prism.Mvvm;
using System.Windows;
using DoorManagementClient.IRepository;
using System.Collections.ObjectModel;
using DoorManagementClient.ClientTransporter;

namespace DoorManagementClient.ViewModels
{
    sealed class HomeViewModel : BindableBase
    {
        private string _DoorDescription;
        private string _ConnectionStatus;
        private ObservableCollection<Data.DOOR> _Doors;
        public HomeViewModel(IDoorRepository doorRepository, SignalRMessageService service)
        {
            DoorRepository = doorRepository;
            AddCommand = new DelegateCommand(AddDoor);
            RemoveCommand = new DelegateCommand(Remove);
            OpenChangeCommand = new DelegateCommand(OpenStatusChange);
            LockChangeCommand = new DelegateCommand(LockStatusChange);
            SelectedDoor = new Data.DOOR();
            Doors = new ObservableCollection<Data.DOOR>();
            DoorDescription = string.Empty;
            Load();
            signalRMessageService = service;

            signalRMessageService.Connect().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    ConnectionStatus = "Unable to connect server";
                }
                else
                {
                    ConnectionStatus = "Connected to server";
                }
            });

            signalRMessageService.MessageReceived += SignalRMessageService_MessageReceived;
        }


        public SignalRMessageService signalRMessageService { get; set; }
        public IDoorRepository DoorRepository { get; set; }
        public ObservableCollection<Data.DOOR> Doors
        {
            get { return _Doors; }
            set { SetProperty(ref _Doors, value); }
        }
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand OpenChangeCommand { get; }
        public ICommand LockChangeCommand { get; }
        public string DoorDescription
        {
            get { return _DoorDescription; }
            set { SetProperty(ref _DoorDescription, value); }
        }
        public string ConnectionStatus
        {
            get { return _ConnectionStatus; }
            set { SetProperty(ref _ConnectionStatus, value); }
        }
        public Data.DOOR SelectedDoor { get; set; }


        public void Load()
        {
            Doors.Clear();
            DoorRepository.GetAllDoor().ToList().ForEach(x => Doors.Add(x));
        }
        private void SignalRMessageService_MessageReceived(string obj)
        {
            var response = obj.Split('-').ToList();
            if (response[0].ToString() == "1")
            {
                Doors.Clear();
                DoorRepository.GetAllDoor().ToList().ForEach(x => Doors.Add(x));
            }
            else
            {
                var tempDoor = Doors.ToList();
                Doors.Clear();
                foreach (var door in tempDoor)
                {
                    if(door.DoorId == Convert.ToInt32(response[1].ToString()))
                    {
                        if (door.Opened != Convert.ToBoolean(Convert.ToInt16(response[2].ToString())))
                            door.Opened = Convert.ToBoolean(Convert.ToInt16(response[2].ToString()));

                        if (door.Locked != Convert.ToBoolean(Convert.ToInt16(response[3].ToString())))
                            door.Locked = Convert.ToBoolean(Convert.ToInt16(response[3].ToString()));
                    }
                    Doors.Add(door);
                }
            }
        }
        public void AddDoor()
        {
            if (string.IsNullOrEmpty(DoorDescription))
            {
                MessageBox.Show("Enter Door Description", "Door Management");
            }
            else
            {
                DoorRepository.AddDoor(new Data.DOOR() { Description = DoorDescription, Locked = false, Opened = true });
                DoorDescription = string.Empty;
                MessageBox.Show("New Door Added successfully", "Door Management");
                SendMessage(true);

            }
        }
        public void Remove()
        {
            DoorRepository.DeleteDoor(SelectedDoor.DoorId);
            MessageBox.Show("Door Removed successfully", "Door Management");
            SendMessage(true);
        }
        public void OpenStatusChange()
        {
            SelectedDoor.Opened = !SelectedDoor.Opened;
            DoorRepository.UpdateDoor(SelectedDoor);
            var message = SelectedDoor.Opened.Value ? "Door Opened Successfully" : "Door Closed Successfully";
            MessageBox.Show(message, "Door Management");
            SendMessage();
        }
        public void LockStatusChange()
        {
            SelectedDoor.Locked = !SelectedDoor.Locked;
            DoorRepository.UpdateDoor(SelectedDoor);
            var message = SelectedDoor.Locked.Value ? "Door Locked Successfully" : "Door UnLocked Successfully";
            MessageBox.Show(message, "Door Management");
            SendMessage();
        }
        public string connectionID = string.Empty;
        public async void SendMessage(bool isReload = false)
        {
            string value = string.Empty;
           
            if (isReload)
            {
                value += "1-0-0-0";
            }
            else
            {
               value += "0-";
               value += SelectedDoor.DoorId;
                value += SelectedDoor.Opened.Value ? "-1" : "-0";
                value += SelectedDoor.Locked.Value ? "-1" : "-0";
            }
            await signalRMessageService.SendMessage(value);
        }
    }
}
